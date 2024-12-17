﻿using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Lab_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestResultRepository _testResultRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository,ITestResultRepository testResultRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _testResultRepository = testResultRepository;
            _logger = logger;
        }

        [HttpGet("user-details")]
        public async Task<IActionResult> GetAuthenticatedUserDetails()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return Unauthorized(new FailureResult("Yetkisiz erisim."));
            var result = await _userRepository.GetUserDetails(idClaim.Value);
            return result is SuccessDataResult<UserDto> ?
                Ok(result) : BadRequest(result);
        }

        [HttpGet("user-test-results")]
        public async Task<IActionResult> GetAuthenticatedUserTestResults()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return Unauthorized(new FailureResult("Yetkisiz erisim."));
            
            var result= await _testResultRepository.GetAllTestResultsOfUser(idClaim.Value);
            return result is FailureResult ? BadRequest(result) : Ok(result);
        }


        [HttpDelete("remove-account")]
        public async Task<IActionResult> RemoveAuthenticatedUser()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return Unauthorized(new FailureResult("Yetkiniz bulunmamaktadır."));

            var result = await _userRepository.DeleteUser(idClaim.Value);
            return result is SuccessResult ? Ok(result) : BadRequest(result);
        }
    }
}
