using E_Lab_Backend.Data;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Lab_Backend.Services
{
    public interface IJwtService
    {
        string GenerateJwt(string userId, string role);
        Task<ResultModel> RefreshJwt(string refreshToken);
        Task<string> GenerateRefreshToken(string userId);
        Task<ResultModel> RevokeRefreshToken(string userId);
    }

    public class JwtService : IJwtService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public JwtService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateJwt(string userId, string role)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(string userId)
        {
            var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);
            if (existingToken != null)
            {
                _context.RefreshTokens.Remove(existingToken);
                await _context.SaveChangesAsync();
            }
            var refreshToken = new RefreshToken(userId);
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken.Token;
        }

        public async Task<ResultModel> RefreshJwt(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                return new FailureResult("Refresh token gecersiz veya süresi bitmiş.");

            return new SuccessDataResult<string>(GenerateJwt(token.Id, "User"));
        }

        public async Task<ResultModel> RevokeRefreshToken(string userId)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId);

            if (token == null)
                return new FailureResult("Kullanıcı için aktif bir refresh token bulunamadı.");

            token.IsRevoked = true;
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
            return new SuccessResult("Token iptali sağlandı.");
        }
    }
}
