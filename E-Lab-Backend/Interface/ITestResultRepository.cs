using E_Lab_Backend.Dto;

namespace E_Lab_Backend.Interface
{
    public interface ITestResultRepository
    {
        Task<ResultModel> GetUserTestResults(string userId);
        Task<ResultModel> GetAllTestResults();  
        Task<ResultModel> GetTestResultById(string resultId);
        Task<ResultModel> AddNewTestResult(NewTestResultDto dto);
    }
}
