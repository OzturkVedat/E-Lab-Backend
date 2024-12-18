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

        /// <summary>
        /// Tüm hastaların detaylarını getirir.
        /// </summary>
        /// <returns>Hastaların bilgilerini içeren bir liste.</returns>
        [HttpGet("all-patient-details")]
        public async Task<IActionResult> GetAllPatientDetails()
        {
            var result = await _userRepository.GetAllPatientDetails();
            return result is SuccessDataResult<List<PatientDetails>> ?
                Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Belirtilen hasta ID'sine ait tüm tahlil sonuçlarını getirir.
        /// </summary>
        /// <param name="patientId">Hasta ID'si.</param>
        /// <returns>Tahlil sonuçları listesi.</returns>
        [HttpGet("patient-test-results/{patientId}")]
        public async Task<IActionResult> GetPatientTestResultsById(string patientId)
        {
            if (patientId.IsNullOrEmpty())
                return BadRequest(new FailureResult("patientId degeri bos olamaz."));

            var results = await _testResultRepository.GetAllTestResultsOfUser(patientId);
            return results is FailureResult ?
                BadRequest(results) : Ok(results);
        }

        /// <summary>
        /// Belirtilen tahlil sonucu ID'sinin detaylarını ve kılavuzlardaki sonuçlarını getirir.
        /// </summary>
        /// <param name="testResultId">Tahlil sonucu ID'si.</param>
        /// <returns>Tahlil sonucu detayları.</returns>
        [HttpGet("test-result-details/{testResultId}")]
        public async Task<IActionResult> GetTestResultDetailsById(string testResultId)
        {
            if (testResultId.IsNullOrEmpty())
                return BadRequest(new FailureResult("testResultId degeri bos olamaz."));

            var detailsResult= await _testResultRepository.GetTestResultDetails(testResultId);
            return detailsResult is FailureResult ? BadRequest(detailsResult) : Ok(detailsResult);
        }

        /// <summary>
        /// Aynı hasta için belirtilen tahlil sonucundan önceki iki tahlil sonucunu (varsa) getirir.
        /// </summary>
        /// <param name="testResultId">Tahlil sonucu ID'si.</param>
        /// <returns>Önceki tahlil sonuçları.</returns>
        [HttpGet("previous-test-results/{testResultId}")]
        public async Task<IActionResult> GetPreviousTestResults(string testResultId)
        {
            if (testResultId.IsNullOrEmpty())
                return BadRequest(new FailureResult("testResultId degeri bos olamaz."));

            var prevResults= await _testResultRepository.GetPreviousTestResults(testResultId);
            return prevResults is FailureResult ? BadRequest(prevResults) : Ok(prevResults);
        }

        /// <summary>
        /// Kayıtlı tüm tahlil sonuçlarını getirir.
        /// </summary>
        /// <returns>Tüm tahlil sonuçlarının bir listesi.</returns>
        [HttpGet("all-test-results")]
        public async Task<IActionResult> GetAllTestResults()
        {

            var results = await _testResultRepository.GetAllTestResults(); 
            return results is FailureResult ? BadRequest(results):Ok(results);
        }

        /// <summary>
        /// Yeni bir tahlil sonucu kaydı ekler.
        /// </summary>
        /// <param name="dto">Yeni tahlil sonucu bilgileri.</param>
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
