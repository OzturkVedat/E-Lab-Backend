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
        /// <returns>Hastaların bilgilerini içeren liste</returns>
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
        /// <returns>Tahlil sonuçları listesi</returns>
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
        /// Belirtilen hasta ismine ait tüm sonuçlarını getirir.
        /// </summary>
        /// <param name="fullName">Hasta ad soyadı.</param>
        [HttpGet("patient-by-fullname/{fullName}")]
        public async Task<IActionResult> GetPatientByFullName(string fullName)
        {
            if (fullName.IsNullOrEmpty())
                return BadRequest(new FailureResult("Ad soyad bos olamaz."));

            var results = await _userRepository.GetUserByFullName(fullName);
            return results is FailureResult ?
                BadRequest(results) : Ok(results);
        }

        /// <summary>
        /// Aynı hasta için belirtilen tahlil sonucundan önceki iki tahlil sonucunu (varsa) getirir.
        /// </summary>
        /// <param name="testResultId">Tahlil sonucu ID'si.</param>
        /// <returns>Önceki tahlil sonuçları</returns>
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
        /// <returns>Tüm tahlil sonuçlarının bir listesi</returns>
        [HttpGet("all-test-results")]
        public async Task<IActionResult> GetAllTestResults()
        {

            var results = await _testResultRepository.GetAllTestResults(); 
            return results is FailureResult ? BadRequest(results):Ok(results);
        }


        /// <summary>
        /// Yaş ve Ig degerlerinin kilavuzlarda sonuçlarını getirir.
        /// </summary>
        /// <returns>Kılavuz sonuçları</returns>
        [HttpPost("check-manual")]
        public async Task<IActionResult> GetManualResultsByAgeAndIgs([FromBody] CheckManualDto dto)
        {
            if (dto == null || dto.AgeInMonths < 0)
                return BadRequest(new FailureResult("Gecersiz sorgu giris verisi."));

            var result = await _testResultRepository.GetManualResultsByAgeAndIgs(dto);
            return result is FailureResult ? BadRequest(result) : Ok(result);
        }

        /// <summary>
        /// Yeni bir tahlil sonucu kaydı ekler.
        /// </summary>
        /// <param name="dto">Yeni tahlil sonucu bilgileri</param>
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
