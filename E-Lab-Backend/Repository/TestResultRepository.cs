using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Lab_Backend.Data;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TestResultRepository> _logger;
        public TestResultRepository(ApplicationDbContext context, IMapper mapper, ILogger<TestResultRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultModel> GetTestResultDetails(string resultId)
        {
            try
            {
                var result = await _context.TestResults
                    .AsNoTracking()
                    .Where(r => r.Id == resultId)
                    .ProjectTo<TestResultDetails>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (result == null)
                    return new FailureResult("Tahlil bulunamadi.");

                var nonNulls = ExtractNonNullFloatValues(result);

                var rangeResults = await CheckManuals(nonNulls, result.AgeInMonths);
                return new SuccessDataResult<List<Dictionary<IgTypeEnum, (string, float)>>>(rangeResults);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error fetching test result.");
                return new FailureResult("Tahlil sonucu sorgusunda bir hata olustu.");
            }
        }

        public async Task<ResultModel> GetAllTestResultsOfUser(string userId)
        {
            try
            {
                var results = await _context.TestResults
                .AsNoTracking()
                .Where(r => r.PatientId == userId)
                .OrderByDescending(r => r.ExpertApproveTime)        // latest to oldest
                .ProjectTo<TestResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

                return new SuccessDataResult<List<TestResultDto>>(results);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error fetching test results for user {UserId}", userId);
                return new FailureResult("Tahlil sonuclari sorgusunda bir hata olustu.");
            }
        }


        public async Task<ResultModel> AddNewTestResult(NewTestResultDto dto)
        {
            try
            {
                var user = await _context.Users.FindAsync(dto.PatientId);
                if (user == null)
                    return new FailureResult("Tahlil sonucunun eklenecegi hasta kaydi bulunamadi.");

                var newTestRes = _mapper.Map<TestResult>(dto);
                await _context.TestResults.AddAsync(newTestRes);
                await _context.SaveChangesAsync();
                return new SuccessResult("Tahlil sonucu basariyla kaydedildi.");

            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error adding new test result.");
                return new FailureResult("Yeni tahlil sonucu eklenirken hata olustu.");
            }
        }

        private Dictionary<IgTypeEnum, float> ExtractNonNullFloatValues(TestResultDetails testResult)
        {
            return testResult.GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(float?))
                .Select(p => new
                {
                    Key = GetIgTypeEnumFromPropertyName(p.Name),
                    Value = (float?)p.GetValue(testResult)
                })
                .Where(x => x.Value.HasValue)   // keep only non-nulls
                .ToDictionary(x => x.Key, x => x.Value.Value);
        }
        private IgTypeEnum GetIgTypeEnumFromPropertyName(string propertyName)
        {
            return propertyName switch
            {
                "IgA" => IgTypeEnum.IgA,
                "IgM" => IgTypeEnum.IgM,
                "IgG" => IgTypeEnum.IgG,
                "IgG1" => IgTypeEnum.IgG1,
                "IgG2" => IgTypeEnum.IgG2,
                "IgG3" => IgTypeEnum.IgG3,
                "IgG4" => IgTypeEnum.IgG4,
                _ => throw new ArgumentException($"Unknown property name: {propertyName}")
            };
        }

        private async Task<List<Dictionary<IgTypeEnum, (string, float)>>> CheckManuals(Dictionary<IgTypeEnum, float> nonNullValues, int ageInMonths)
        {
            var igManualApTask = _context.IgsManualAp
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();

            var igManualCilvPrimerTask = _context.IgsManualCilvPrimer
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();

            var igManualCilvSeconderTask = _context.IgsManualCilvSeconder
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();

            // parallel async
            await Task.WhenAll(igManualApTask, igManualCilvPrimerTask, igManualCilvSeconderTask);

            var igManualAp = igManualApTask.Result;
            var igManualCilvPrimer = igManualCilvPrimerTask.Result;
            var igManualCilvSeconder = igManualCilvSeconderTask.Result;

            var igManualApResults = new Dictionary<IgTypeEnum, (string, float)>();
            var igManualCilvResults = new Dictionary<IgTypeEnum, (string, float)>();

            foreach (var (key, value) in nonNullValues)
            {
                if (igManualAp != null)
                {
                    string rangeResult = key switch
                    {
                        IgTypeEnum.IgA => GetRangeResult(value, igManualAp.IgALowerLimit, igManualAp.IgAUpperLimit),
                        IgTypeEnum.IgM => GetRangeResult(value, igManualAp.IgMLowerLimit, igManualAp.IgMUpperLimit),
                        IgTypeEnum.IgG => GetRangeResult(value, igManualAp.IgGLowerLimit, igManualAp.IgGUpperLimit),
                        IgTypeEnum.IgG1 => GetRangeResult(value, igManualAp.IgG1LowerLimit, igManualAp.IgG1UpperLimit),
                        IgTypeEnum.IgG2 => GetRangeResult(value, igManualAp.IgG2LowerLimit, igManualAp.IgG2UpperLimit),
                        IgTypeEnum.IgG3 => GetRangeResult(value, igManualAp.IgG3LowerLimit, igManualAp.IgG3GUpperLimit),
                        IgTypeEnum.IgG4 => GetRangeResult(value, igManualAp.IgG4LowerLimit, igManualAp.IgG4UpperLimit),
                        _ => "No Limit Defined"
                    };
                    igManualApResults.Add(key, (rangeResult, value));
                }
                if (igManualCilvPrimer != null)
                {
                    string rangeResult = key switch
                    {
                        IgTypeEnum.IgA => GetRangeResult(value, igManualCilvPrimer.IgALowerLimit, igManualCilvPrimer.IgAUpperLimit),
                        IgTypeEnum.IgM => GetRangeResult(value, igManualCilvPrimer.IgMLowerLimit, igManualCilvPrimer.IgMUpperLimit),
                        IgTypeEnum.IgG => GetRangeResult(value, igManualCilvPrimer.IgGLowerLimit, igManualCilvPrimer.IgGUpperLimit),
                        _ => "No Limit Defined"
                    };
                    igManualCilvResults.Add(key, (rangeResult, value));
                }
                if (igManualCilvSeconder != null)
                {
                    string rangeResult = key switch
                    {
                        IgTypeEnum.IgG1 => GetRangeResult(value, igManualCilvSeconder.IgG1LowerLimit, igManualCilvSeconder.IgG1UpperLimit),
                        IgTypeEnum.IgG2 => GetRangeResult(value, igManualCilvSeconder.IgG2LowerLimit, igManualCilvSeconder.IgG2UpperLimit),
                        IgTypeEnum.IgG3 => GetRangeResult(value, igManualCilvSeconder.IgG3LowerLimit, igManualCilvSeconder.IgG3GUpperLimit),
                        IgTypeEnum.IgG4 => GetRangeResult(value, igManualCilvSeconder.IgG4LowerLimit, igManualCilvSeconder.IgG4UpperLimit),
                        _ => "No Limit Defined"
                    };
                    igManualCilvResults.Add(key, (rangeResult, value));
                }
            }
            return new List<Dictionary<IgTypeEnum, (string, float)>> { igManualApResults, igManualCilvResults };
        }


        private static string GetRangeResult(float value, float lowerLimit, float upperLimit)
        {
            if (value < lowerLimit) return "Dusuk";
            if (value > upperLimit) return "Yuksek";
            return "Normal";
        }


    }
}
