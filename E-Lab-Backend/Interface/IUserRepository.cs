using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;

namespace E_Lab_Backend.Interface
{
    public interface IUserRepository
    {
        Task<ResultModel> GetUserById(string id);
        Task<ResultModel> GetUserByEmail(string email);
        Task<ResultModel> AddUser(UserModel user);
    }
}
