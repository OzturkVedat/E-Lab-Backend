using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;
using E_Lab_Backend.Interface;

namespace E_Lab_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ITestResultRepository _testResultRepository;
        private readonly IUserRepository _userRepository;
        public AdminController(ITestResultRepository testResultRepository,IUserRepository userRepository)
        {
            _testResultRepository = testResultRepository;
            _userRepository = userRepository;
        }

        [HttpGet("test-result/{resultId}")]
        public async Task<IActionResult> GetTestResultById(string resultId)
        {
            if (resultId.IsNullOrEmpty())
                return BadRequest(new FailureResult("Invalid ID."));

            var result= await _testResultRepository.GetTestResultById(resultId);
            return result is SuccessDataResult<TestResultPatient> ? Ok(result) : BadRequest(result);
        }

        [HttpGet("all-test-results")]
        public async Task<IActionResult> GetAllTestResults()
        {
            var result = await _testResultRepository.GetAllTestResults();
            return result is SuccessDataResult<List<TestResultPatient>> ?
                Ok(result) : BadRequest(result);
        }

        [HttpGet("all-patient-details")]
        public async Task<IActionResult> GetAllPatientDetails()
        {
            var result= await _userRepository.GetAllPatientDetails();
            return result is SuccessDataResult<List<PatientDetails>> ?
                Ok(result) : BadRequest(result);
        }

        [HttpPost("new-test-result")]
        public async Task<IActionResult> AddNewTestResult(NewTestResultDto dto)
        {
            var result = await _testResultRepository.AddNewTestResult(dto);
            return result is SuccessResult ? Ok(result) : BadRequest(result);
        }

    }
}
