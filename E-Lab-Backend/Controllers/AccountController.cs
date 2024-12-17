using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace E_Lab_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<AccountController> _logger;
        public AccountController( IUserRepository userRepository, IJwtService jwtService,
             IPasswordHasher passwordHasher, ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(new FailureResult(errorMessages));
            }
            var userResult = await _userRepository.GetUserByEmail(dto.Email);
            if (userResult.IsSucess)
            {
                return BadRequest(new FailureResult("Kullanıcı zaten mevcut."));
            }
            var hashedPassword = _passwordHasher.HashPassword(dto.Password);

            var user = new UserModel
            {
                Email = dto.Email,
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                PasswordHashed = hashedPassword
            };
            var result = await _userRepository.AddUser(user);
            return result.IsSucess
                ? Ok(new SuccessResult("Kullanıcı başarıyla kaydedildi."))
                : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto dto)
        {

            if (!ModelState.IsValid)
            {
                var errorMessages = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                _logger.LogWarning("{Email} icin hatalar var: {Errors}", dto.Email, errorMessages);
                return BadRequest(new FailureResult(errorMessages));
            }

            var userCheck = await _userRepository.GetUserByEmail(dto.Email);
            if (userCheck is not SuccessDataResult<UserModel> user)
                return Unauthorized(new FailureResult("Geçersiz kimlik bilgileri."));

            var isPasswordValid = _passwordHasher.VerifyPassword(dto.Password, user.Data.PasswordHashed);
            if (!isPasswordValid)            
                return Unauthorized(new FailureResult("Geçersiz kimlik bilgileri."));
            

            var userId = user.Data.Id;
            var role= user.Data.Role;
            var accessToken = _jwtService.GenerateJwt(userId, role);
            var refreshToken = await _jwtService.GenerateRefreshToken(userId);

            var response = new AuthResponse
            {
                UserId = userId,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(new SuccessDataResult<AuthResponse>(response));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (dto.Token.IsNullOrEmpty())
                return BadRequest(new FailureResult("Geçersiz token."));

            var result = await _jwtService.RefreshJwt(dto.Token);
            return result is SuccessDataResult<string> success ?
              Ok(success) : BadRequest(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new FailureResult("Kullanıcı kimlik doğrulaması yapılmadı."));

            var revokeResult = await _jwtService.RevokeRefreshToken(userIdClaim.Value);
            return revokeResult.IsSucess ? Ok(revokeResult) : BadRequest(revokeResult);
        }
    }
}
