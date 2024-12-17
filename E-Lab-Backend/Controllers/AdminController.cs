using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;
using E_Lab_Backend.Interface;
using System.Reflection.Metadata.Ecma335;

namespace E_Lab_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestResultRepository _testResultRepository;
        public AdminController(IUserRepository userRepository, ITestResultRepository testResultRepository)
        {
            _userRepository = userRepository;
            _testResultRepository = testResultRepository;
        }

        [HttpGet("all-patient-details")]
        public async Task<IActionResult> GetAllPatientDetails()
        {
            var result = await _userRepository.GetAllPatientDetails();
            return result is SuccessDataResult<List<PatientDetails>> ?
                Ok(result) : BadRequest(result);
        }

        [HttpGet("patient-test-results/{patientId}")]
        public async Task<IActionResult> GetPatientTestResultsById(string patientId)
        {
            if (patientId.IsNullOrEmpty())
                return BadRequest(new FailureResult("patientId degeri bos olamaz."));

            var results = await _testResultRepository.GetAllTestResultsOfUser(patientId);
            return results is FailureResult ?
                BadRequest(results) : Ok(results);
        }

        [HttpGet("test-result-details/{testResultId}")]
        public async Task<IActionResult> GetTestResultDetailsById(string testResultId)
        {
            if (testResultId.IsNullOrEmpty())
                return BadRequest(new FailureResult("testResultId degeri bos olamaz."));

            var detailsResult= await _testResultRepository.GetTestResultDetails(testResultId);
            return detailsResult is FailureResult ? BadRequest(detailsResult) : Ok(detailsResult);
        }

        [HttpPost("new-test-result")]
        public async Task<IActionResult> AddNewTestResult([FromBody] NewTestResultDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                return BadRequest(new FailureResult("Validasyon hatasi.", errors));
            }

            var result = await _testResultRepository.AddNewTestResult(dto);
            return result is SuccessResult ?
                Ok(result) : BadRequest(result);
        }


    }
}
