using E_Lab_Backend.Data;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResultModel> GetUserById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return (user == null) ?
            new FailureResult("Kullanici bulunamadı.") : new SuccessDataResult<UserModel>(user);
        }

        public async Task<ResultModel> GetUserDetails(string id)
        {
            var details = await _context.Users
                .Where(u => u.Id == id)     // filter
                .Select(u => new UserDto    // project
                {
                    FullName = u.FullName,
                    Email = u.Email,
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
                    Email = u.Email
                })
                .ToListAsync();
            return new SuccessDataResult<List<PatientDetails>>(details);
        }

        public async Task<ResultModel> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)
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
