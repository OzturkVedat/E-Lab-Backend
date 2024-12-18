using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace E_Lab_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly ITestResultRepository _testResultRepository;
        public UserController(ITestResultRepository testResultRepository)
        {
            _testResultRepository = testResultRepository;
        }

        /// <summary>
        /// Giriş yapmış kullanıcının tüm tahlil sonuçlarını getirir.
        /// </summary>
        /// <returns>Tahlil sonuçları başarıyla getirildiyse 200 OK, aksi takdirde hata mesajı ile 400 BadRequest döner.</returns>
        [HttpGet("user-test-results")]
        public async Task<IActionResult> GetAuthenticatedUserTestResults()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return Unauthorized(new FailureResult("Yetkisiz erisim."));
            
            var result= await _testResultRepository.GetAllTestResultsOfUser(idClaim.Value);
            return result is FailureResult ? BadRequest(result) : Ok(result);
        }

        /// <summary>
        /// Verilen tahlil sonucu ID'sine göre tahlil detaylarını ve kılavuzlardaki sonuçlarını getirir.
        /// </summary>
        /// <param name="testResultId">Tahlil sonucu ID'si.</param>
        [HttpGet("test-result-details/{testResultId}")]
        public async Task<IActionResult> GetTestResultDetailsById(string testResultId)
        {
            if (testResultId.IsNullOrEmpty())
                return BadRequest(new FailureResult("testResultId degeri bos olamaz."));

            var detailsResult = await _testResultRepository.GetTestResultDetails(testResultId);
            return detailsResult is FailureResult ? BadRequest(detailsResult) : Ok(detailsResult);
        }

    }
}
