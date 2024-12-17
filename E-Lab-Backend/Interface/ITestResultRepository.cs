using E_Lab_Backend.Dto;

namespace E_Lab_Backend.Interface
{
    public interface ITestResultRepository
    {
        Task<ResultModel> GetTestResultDetails(string resultId);
        Task<ResultModel> GetAllTestResultsOfUser(string userId);
        Task<ResultModel> AddNewTestResult(NewTestResultDto dto);
    }
}
