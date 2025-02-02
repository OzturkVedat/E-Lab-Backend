﻿using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;

namespace E_Lab_Backend.Interface
{
    public interface IUserRepository
    {
        Task<ResultModel> GetUserById(string id);
        Task<ResultModel> GetUserByFullName(string fullname);
        Task<ResultModel> GetUserDetails(string id);
        Task<ResultModel> GetAllPatientDetails();
        Task<ResultModel> GetUserByTckn(string tckn);
        Task<bool> UserExists(string userId);
        Task<ResultModel> AddUser(UserModel user);
        Task<ResultModel> UpdateUserDetails(string userId, UserUpdateDto dto);
        Task<ResultModel> DeleteUser(string id);
    }
}
