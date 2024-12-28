using E_Lab_Backend.Data;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultModel> GetUserById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return (user == null) ?
            new FailureResult("Kullanici bulunamadı.") : new SuccessDataResult<UserModel>(user);
        }

        public async Task<ResultModel> GetUserByFullName(string fullname)
        {
            var results = await _context.Users
                .Where(u => EF.Functions.Like(u.FullName, $"%{fullname}%"))
                .Take(20)
                .ToListAsync();

            var searchResults = results.Select(user => new PatientDetails
            {
                PatientId = user.Id,
                FullName = user.FullName,
                Tckn = user.Tckn,
                Gender=user.Gender,
            }).ToList();
            return new SuccessDataResult<List<PatientDetails>>(searchResults);
        }

        public async Task<ResultModel> GetUserDetails(string id)
        {
            var details = await _context.Users
                .Where(u => u.Id == id)     // filter
                .Select(u => new UserDto    // project
                {
                    FullName = u.FullName,
                    Tckn = u.Tckn,
                    BirthDate = u.BirthDate
                })
                .FirstOrDefaultAsync();

            return (details == null) ?
                new FailureResult("Kullanıcı bulunamadı.") : new SuccessDataResult<UserDto>(details);
        }

        public async Task<ResultModel> GetAllPatientDetails()
        {
            var details = await _context.Users
                .Where(u => u.Role == "User")
                .Select(u => new PatientDetails
                {
                    PatientId = u.Id,
                    FullName = u.FullName,
                    Tckn = u.Tckn,
                    Gender=u.Gender
                })
                .ToListAsync();
            return new SuccessDataResult<List<PatientDetails>>(details);
        }

        public async Task<ResultModel> GetUserByTckn(string tckn)
        {
            var user = await _context.Users
                .Where(u => u.Tckn == tckn)
                .FirstOrDefaultAsync();

            return (user == null) ?
            new FailureResult("Kullanici bulunamadi.") : new SuccessDataResult<UserModel>(user);
        }

        public async Task<bool> UserExists(string userId)
        {
            return await _context.Users.AnyAsync(user => user.Id == userId);
        }

        public async Task<ResultModel> AddUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return result > 0 ?
                new SuccessResult("Kullanici başarıyla kaydedildi.") : new FailureResult("Kullanici kaydedilemedi.");
        }

        public async Task<ResultModel> UpdateUserDetails(string userId, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new FailureResult("Kullanıcı bulunamadı.");

            user.FullName = dto.FullName;
            user.BirthDate = dto.BirthDate;
            var result = await _context.SaveChangesAsync();
            return result > 0 ?
                new SuccessResult("Kullanıcı bilgileri başarıyla güncellendi.") : new FailureResult("Kullanıcı bilgileri güncellenemedi.");
        }

        public async Task<ResultModel> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new FailureResult("Kullanici bulunamadi.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new SuccessResult("Kullanici kaydi basariyla silindi.");
        }
    }
}
