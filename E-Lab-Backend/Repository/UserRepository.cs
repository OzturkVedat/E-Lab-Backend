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
            new FailureResult("Kullanıcı bulunamadı.") : new SuccessDataResult<UserModel>(user);
        }
        public async Task<ResultModel> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return (user == null) ?
            new FailureResult("Kullanıcı bulunamadı.") : new SuccessDataResult<UserModel>(user);
        }

        public async Task<ResultModel> AddUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return result > 0 ?
                new SuccessResult("Kullanıcı başarıyla kaydedildi.") : new FailureResult("Kullanıcı kaydedilemedi.");
        }


    }
}
