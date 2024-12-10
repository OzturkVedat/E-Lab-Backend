using AutoMapper;
using E_Lab_Backend.Data;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TestResultRepository(ApplicationDbContext context, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResultModel> GetUserTestResults(string userId)
        {
            var results = await _context.TestResultsPatient.Where(p => p.PatientId == userId).ToListAsync();
            return (results != null) ? new SuccessDataResult<List<TestResultPatient>>(results) : new FailureResult("Tahlil sonuçları bulunamadı.");
        }
        public async Task<ResultModel> GetTestResultById(string resultId)
        {
            var result = await _context.TestResultsPatient.FindAsync(resultId);
            return (result != null) ? new SuccessDataResult<TestResultPatient>(result) : new FailureResult("Tahlil sonucu bulunamadı.");
        }
        public async Task<ResultModel> GetAllTestResults()
        {
            var results = await _context.TestResultsPatient.ToListAsync();
            return new SuccessDataResult<List<TestResultPatient>>(results);
        }

        public async Task<ResultModel> AddNewTestResult(NewTestResultDto dto)
        {
            var result = await _userRepository.GetUserById(dto.PatientId);
            if (result is SuccessDataResult<UserModel> user)
            {
                var entity = _mapper.Map<TestResultPatient>(dto);
                var ageInM = user.Data.GetAgeInMonths();
                entity.IgAResult = IgReferenceRanges.DetermineResult("IgA", dto.IgA, ageInM);
                entity.IgMResult = IgReferenceRanges.DetermineResult("IgM", dto.IgM, ageInM);
                entity.IgGResult = IgReferenceRanges.DetermineResult("IgG", dto.IgG, ageInM);
                entity.IgG1Result = IgReferenceRanges.DetermineResult("IgG1", dto.IgG1, ageInM);
                entity.IgG2Result = IgReferenceRanges.DetermineResult("IgG2", dto.IgG2, ageInM);
                entity.IgG3Result = IgReferenceRanges.DetermineResult("IgG3", dto.IgG3, ageInM);
                entity.IgG4Result = IgReferenceRanges.DetermineResult("IgG4", dto.IgG4, ageInM);

                await _context.TestResultsPatient.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new SuccessResult("Yeni tahlil sonucu başarıyla eklendi.");
            }
            else
                return new FailureResult("Tahlil kaydı başarısız. Bu tahlile ait hastanın kaydı bulunamadı.");

        }

    }
}
