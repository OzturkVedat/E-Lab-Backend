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

        public async Task<ResultModel> GetManualResultsByAgeAndIgs(CheckManualDto dto)
        {
            try
            {
                var details = _mapper.Map<TestResultDetails>(dto);
                var manResults = await CheckManuals(details);
                return new SuccessDataResult<TestResultsFromManuals>(manResults);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching manual results.");
                return new FailureResult("Kilavuz sonucu sorgusunda bir hata olustu.");
            }   
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

                var manualResults = await CheckManuals(result);
                return new SuccessDataResult<TestResultsFromManuals>(manualResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching test result.");
                return new FailureResult("Tahlil sonucu sorgusunda bir hata olustu.");
            }
        }

        public async Task<ResultModel> GetPreviousTestResults(string resultId)
        {
            try
            {
                var currentResult = await _context.TestResults.FindAsync(resultId);
                if (currentResult == null)
                    return new FailureResult("Tahlil bulunamadi.");

                var previousResults = await _context.TestResults
                    .Where(tr => tr.PatientId == currentResult.PatientId && tr.Id != resultId)
                    .OrderByDescending(tr => tr.ExpertApproveTime)
                    .Take(2)
                    .ProjectTo<TestResultDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return new SuccessDataResult<List<TestResultDto>>(previousResults);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching previous test results.");
                return new FailureResult("Gecmis tahlil sonucu sorgusu/karsilastirmasinda bir hata olustu.");
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


        public async Task<ResultModel> GetAllTestResults()
        {
            try
            {
                var results = await _context.TestResults
                .AsNoTracking()
                .OrderByDescending(r => r.ExpertApproveTime)
                .ProjectTo<TestResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

                return new SuccessDataResult<List<TestResultDto>>(results);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error fetching all test results.");
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

        private async Task<TestResultsFromManuals> CheckManuals(TestResultDetails details)
        {
            var ageInMonths = details.AgeInMonths;

            var igManualAp = await _context.IgsManualAp
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();
            var igResultAp = new IgResultsDto { ReferencedManualName = "ManualAp" };

            var igManualCilvPrimer = await _context.IgsManualCilvPrimer
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();
            var igManualCilvSeconder = await _context.IgsManualCilvSeconder
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .FirstOrDefaultAsync();
            var igResultCilv = new IgResultsDto { ReferencedManualName = "ManualCilv" };

            var igManualOs = await _context.IgsManualOs
                .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
                .ToListAsync();
            var igResultOs = new ManualOsResult();
            var igRangesOsList = new List<IgOsRangesResult>();

            var igManualTjp = await _context.IgsManualTjp
               .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
               .ToListAsync();
            var igResultTjp = new ManualTjpResult();
            var igRangesTjpList = new List<IgTjpRangesResult>();

            var igManualTubitak = await _context.IgsManualTubitak
               .Where(ig => ageInMonths >= ig.AgeInMonthsLowerLimit && ageInMonths <= ig.AgeInMonthsUpperLimit)
               .ToListAsync();
            var igResultTubitak = new ManualTubitakResult();
            var igRangesTubitakList = new List<IgTubitakRangesResult>();

            if (details.IgG != null)
            {
                igResultAp.IgGResult = GetRangeResult(details.IgG, igManualAp.IgGLowerLimit, igManualAp.IgGUpperLimit);
                igResultCilv.IgGResult = GetRangeResult(details.IgG, igManualCilvPrimer.IgGLowerLimit, igManualCilvPrimer.IgGUpperLimit);

                var iggOsRange = igManualOs.Where(ig => ig.IgType == IgTypeEnum.IgG).FirstOrDefault();
                IgOsRangesResult iggOsResult = new IgOsRangesResult
                {
                    IgType = IgTypeEnum.IgG,
                    MinMaxResult = GetRangeResult(details.IgG, iggOsRange.MinValue, iggOsRange.MaxValue),
                    ArithMeanResult = GetRangeResult(details.IgG, (iggOsRange.ArithmeticMean - iggOsRange.AMStandardDeviation), (iggOsRange.ArithmeticMean + iggOsRange.AMStandardDeviation))
                };
                igRangesOsList.Add(iggOsResult);

                var iggTjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgG).FirstOrDefault();
                IgTjpRangesResult iggTjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgG,
                    MinMaxResult = GetRangeResult(details.IgG, iggTjpRange.MinValue, iggTjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgG, (iggTjpRange.GeometricMean - iggTjpRange.GMStandardDeviation), (iggTjpRange.GeometricMean + iggTjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG, iggTjpRange.CILowerLimit, iggTjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(iggTjpResult);

                var iggTubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgG).FirstOrDefault();
                IgTubitakRangesResult iggTubResult = new IgTubitakRangesResult
                {
                    IgType = IgTypeEnum.IgG,
                    MinMaxResult = GetRangeResult(details.IgG, iggTubRange.MinValue, iggTubRange.MaxValue),
                    MeanResult = GetRangeResult(details.IgG, (iggTubRange.Mean - iggTubRange.MeanStandardDeviation), (iggTubRange.Mean + iggTubRange.MeanStandardDeviation)),
                    GMResult = GetRangeResult(details.IgG, (iggTubRange.GeometricMean - iggTubRange.GMStandardDeviation), (iggTubRange.GeometricMean + iggTubRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG, iggTubRange.CILowerLimit, iggTubRange.CIUpperLimit)
                };
                igRangesTubitakList.Add(iggTubResult);

            }
            if (details.IgA != null)
            {
                igResultAp.IgAResult = GetRangeResult(details.IgA, igManualAp.IgALowerLimit, igManualAp.IgAUpperLimit);
                igResultCilv.IgAResult = GetRangeResult(details.IgA, igManualCilvPrimer.IgALowerLimit, igManualCilvPrimer.IgAUpperLimit);

                var igaOsRange = igManualOs.Where(ig => ig.IgType == IgTypeEnum.IgA).FirstOrDefault();
                IgOsRangesResult igaOsResult = new IgOsRangesResult
                {
                    IgType = IgTypeEnum.IgA,
                    MinMaxResult = GetRangeResult(details.IgA, igaOsRange.MinValue, igaOsRange.MaxValue),
                    ArithMeanResult = GetRangeResult(details.IgA, (igaOsRange.ArithmeticMean - igaOsRange.AMStandardDeviation), (igaOsRange.ArithmeticMean + igaOsRange.AMStandardDeviation))
                };
                igRangesOsList.Add(igaOsResult);

                var igaTjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgA).FirstOrDefault();
                IgTjpRangesResult igaTjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgA,
                    MinMaxResult = GetRangeResult(details.IgA, igaTjpRange.MinValue, igaTjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgA, (igaTjpRange.GeometricMean - igaTjpRange.GMStandardDeviation), (igaTjpRange.GeometricMean + igaTjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgA, igaTjpRange.CILowerLimit, igaTjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igaTjpResult);

                var igaTubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgA).FirstOrDefault();
                IgTubitakRangesResult igaTubResult = new IgTubitakRangesResult
                {
                    IgType = IgTypeEnum.IgA,
                    MinMaxResult = GetRangeResult(details.IgA, igaTubRange.MinValue, igaTubRange.MaxValue),
                    MeanResult = GetRangeResult(details.IgA, (igaTubRange.Mean - igaTubRange.MeanStandardDeviation), (igaTubRange.Mean + igaTubRange.MeanStandardDeviation)),
                    GMResult = GetRangeResult(details.IgA, (igaTubRange.GeometricMean - igaTubRange.GMStandardDeviation), (igaTubRange.GeometricMean + igaTubRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgA, igaTubRange.CILowerLimit, igaTubRange.CIUpperLimit)
                };
                igRangesTubitakList.Add(igaTubResult);

            }
            if (details.IgM != null)
            {
                igResultAp.IgMResult = GetRangeResult(details.IgM, igManualAp.IgMLowerLimit, igManualAp.IgMUpperLimit);
                igResultCilv.IgMResult = GetRangeResult(details.IgM, igManualCilvPrimer.IgMLowerLimit, igManualCilvPrimer.IgMUpperLimit);

                var igmOsRange = igManualOs.Where(ig => ig.IgType == IgTypeEnum.IgM).FirstOrDefault();
                IgOsRangesResult igmOsResult = new IgOsRangesResult
                {
                    IgType = IgTypeEnum.IgM,
                    MinMaxResult = GetRangeResult(details.IgM, igmOsRange.MinValue, igmOsRange.MaxValue),
                    ArithMeanResult = GetRangeResult(details.IgM, (igmOsRange.ArithmeticMean - igmOsRange.AMStandardDeviation), (igmOsRange.ArithmeticMean + igmOsRange.AMStandardDeviation))
                };
                igRangesOsList.Add(igmOsResult);

                var igmTjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgM).FirstOrDefault();
                IgTjpRangesResult igmTjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgM,
                    MinMaxResult = GetRangeResult(details.IgM, igmTjpRange.MinValue, igmTjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgM, (igmTjpRange.GeometricMean - igmTjpRange.GMStandardDeviation), (igmTjpRange.GeometricMean + igmTjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgM, igmTjpRange.CILowerLimit, igmTjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igmTjpResult);

                var igmTubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgM).FirstOrDefault();
                IgTubitakRangesResult igmTubResult = new IgTubitakRangesResult
                {
                    IgType = IgTypeEnum.IgM,
                    MinMaxResult = GetRangeResult(details.IgM, igmTubRange.MinValue, igmTubRange.MaxValue),
                    MeanResult = GetRangeResult(details.IgM, (igmTubRange.Mean - igmTubRange.MeanStandardDeviation), (igmTubRange.Mean + igmTubRange.MeanStandardDeviation)),
                    GMResult = GetRangeResult(details.IgM, (igmTubRange.GeometricMean - igmTubRange.GMStandardDeviation), (igmTubRange.GeometricMean + igmTubRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgM, igmTubRange.CILowerLimit, igmTubRange.CIUpperLimit)
                };
                igRangesTubitakList.Add(igmTubResult);
            }

            if (details.IgG1 != null)
            {
                igResultAp.IgG1Result = GetRangeResult(details.IgG1, igManualAp.IgG1LowerLimit, igManualAp.IgG1UpperLimit);
                igResultCilv.IgG1Result = GetRangeResult(details.IgG1, igManualCilvSeconder.IgG1LowerLimit, igManualCilvSeconder.IgG1UpperLimit);

                var igg1TjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgG1).FirstOrDefault();
                IgTjpRangesResult igg1TjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgG1,
                    MinMaxResult = GetRangeResult(details.IgG1, igg1TjpRange.MinValue, igg1TjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgG1, (igg1TjpRange.GeometricMean - igg1TjpRange.GMStandardDeviation), (igg1TjpRange.GeometricMean + igg1TjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG1, igg1TjpRange.CILowerLimit, igg1TjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igg1TjpResult);

                var igg1TubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgG1).FirstOrDefault();
                if(igg1TubRange != null)
                {
                    IgTubitakRangesResult igg1TubResult = new IgTubitakRangesResult
                    {
                        IgType = IgTypeEnum.IgG1,
                        MinMaxResult = GetRangeResult(details.IgG1, igg1TubRange.MinValue, igg1TubRange.MaxValue),
                        MeanResult = GetRangeResult(details.IgG1, (igg1TubRange.Mean - igg1TubRange.MeanStandardDeviation), (igg1TubRange.Mean + igg1TubRange.MeanStandardDeviation)),
                        GMResult = GetRangeResult(details.IgG1, (igg1TubRange.GeometricMean - igg1TubRange.GMStandardDeviation), (igg1TubRange.GeometricMean + igg1TubRange.GMStandardDeviation)),
                        CIResult = GetRangeResult(details.IgG1, igg1TubRange.CILowerLimit, igg1TubRange.CIUpperLimit)
                    };
                    igRangesTubitakList.Add(igg1TubResult);
                }            
            }
            if (details.IgG2 != null)
            {
                igResultAp.IgG2Result = GetRangeResult(details.IgG2, igManualAp.IgG2LowerLimit, igManualAp.IgG2UpperLimit);
                igResultCilv.IgG2Result = GetRangeResult(details.IgG2, igManualCilvSeconder.IgG2LowerLimit, igManualCilvSeconder.IgG2UpperLimit);

                var igg2TjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgG2).FirstOrDefault();
                IgTjpRangesResult igg2TjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgG2,
                    MinMaxResult = GetRangeResult(details.IgG2, igg2TjpRange.MinValue, igg2TjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgG2, (igg2TjpRange.GeometricMean - igg2TjpRange.GMStandardDeviation), (igg2TjpRange.GeometricMean + igg2TjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG2, igg2TjpRange.CILowerLimit, igg2TjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igg2TjpResult);

                var igg2TubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgG2).FirstOrDefault();
                if(igg2TubRange != null)
                {
                    IgTubitakRangesResult igg2TubResult = new IgTubitakRangesResult
                    {
                        IgType = IgTypeEnum.IgG2,
                        MinMaxResult = GetRangeResult(details.IgG2, igg2TubRange.MinValue, igg2TubRange.MaxValue),
                        MeanResult = GetRangeResult(details.IgG2, (igg2TubRange.Mean - igg2TubRange.MeanStandardDeviation), (igg2TubRange.Mean + igg2TubRange.MeanStandardDeviation)),
                        GMResult = GetRangeResult(details.IgG2, (igg2TubRange.GeometricMean - igg2TubRange.GMStandardDeviation), (igg2TubRange.GeometricMean + igg2TubRange.GMStandardDeviation)),
                        CIResult = GetRangeResult(details.IgG2, igg2TubRange.CILowerLimit, igg2TubRange.CIUpperLimit)
                    };
                    igRangesTubitakList.Add(igg2TubResult);
                }             
            }
            if (details.IgG3 != null)
            {
                igResultAp.IgG3Result = GetRangeResult(details.IgG3, igManualAp.IgG3LowerLimit, igManualAp.IgG3UpperLimit);
                igResultCilv.IgG3Result = GetRangeResult(details.IgG3, igManualCilvSeconder.IgG3LowerLimit, igManualCilvSeconder.IgG3UpperLimit);

                var igg3TjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgG3).FirstOrDefault();
                IgTjpRangesResult igg3TjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgG3,
                    MinMaxResult = GetRangeResult(details.IgG3, igg3TjpRange.MinValue, igg3TjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgG3, (igg3TjpRange.GeometricMean - igg3TjpRange.GMStandardDeviation), (igg3TjpRange.GeometricMean + igg3TjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG3, igg3TjpRange.CILowerLimit, igg3TjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igg3TjpResult);

                var igg3TubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgG3).FirstOrDefault();
                if(igg3TubRange != null)
                {
                    IgTubitakRangesResult igg3TubResult = new IgTubitakRangesResult
                    {
                        IgType = IgTypeEnum.IgG3,
                        MinMaxResult = GetRangeResult(details.IgG3, igg3TubRange.MinValue, igg3TubRange.MaxValue),
                        MeanResult = GetRangeResult(details.IgG3, (igg3TubRange.Mean - igg3TubRange.MeanStandardDeviation), (igg3TubRange.Mean + igg3TubRange.MeanStandardDeviation)),
                        GMResult = GetRangeResult(details.IgG3, (igg3TubRange.GeometricMean - igg3TubRange.GMStandardDeviation), (igg3TubRange.GeometricMean + igg3TubRange.GMStandardDeviation)),
                        CIResult = GetRangeResult(details.IgG3, igg3TubRange.CILowerLimit, igg3TubRange.CIUpperLimit)
                    };
                    igRangesTubitakList.Add(igg3TubResult);
                }              
            }
            if (details.IgG4 != null)
            {
                igResultAp.IgG4Result = GetRangeResult(details.IgG4, igManualAp.IgG4LowerLimit, igManualAp.IgG4UpperLimit);
                igResultCilv.IgG4Result = GetRangeResult(details.IgG4, igManualCilvSeconder.IgG4LowerLimit, igManualCilvSeconder.IgG4UpperLimit);

                var igg4TjpRange = igManualTjp.Where(ig => ig.IgType == IgTypeEnum.IgG4).FirstOrDefault();
                IgTjpRangesResult igg4TjpResult = new IgTjpRangesResult
                {
                    IgType = IgTypeEnum.IgG4,
                    MinMaxResult = GetRangeResult(details.IgG4, igg4TjpRange.MinValue, igg4TjpRange.MaxValue),
                    GMResult = GetRangeResult(details.IgG4, (igg4TjpRange.GeometricMean - igg4TjpRange.GMStandardDeviation), (igg4TjpRange.GeometricMean + igg4TjpRange.GMStandardDeviation)),
                    CIResult = GetRangeResult(details.IgG4, igg4TjpRange.CILowerLimit, igg4TjpRange.CIUpperLimit)
                };
                igRangesTjpList.Add(igg4TjpResult);

                var igg4TubRange = igManualTubitak.Where(ig => ig.IgType == IgTypeEnum.IgG4).FirstOrDefault();
                if(igg4TubRange != null)
                {
                    IgTubitakRangesResult igg4TubResult = new IgTubitakRangesResult
                    {
                        IgType = IgTypeEnum.IgG4,
                        MinMaxResult = GetRangeResult(details.IgG4, igg4TubRange.MinValue, igg4TubRange.MaxValue),
                        MeanResult = GetRangeResult(details.IgG4, (igg4TubRange.Mean - igg4TubRange.MeanStandardDeviation), (igg4TubRange.Mean + igg4TubRange.MeanStandardDeviation)),
                        GMResult = GetRangeResult(details.IgG4, (igg4TubRange.GeometricMean - igg4TubRange.GMStandardDeviation), (igg4TubRange.GeometricMean + igg4TubRange.GMStandardDeviation)),
                        CIResult = GetRangeResult(details.IgG4, igg4TubRange.CILowerLimit, igg4TubRange.CIUpperLimit)
                    };
                    igRangesTubitakList.Add(igg4TubResult);
                }           
            }

            igResultOs.IgOsRangesResults = igRangesOsList;
            igResultTjp.IgTjpRangesResults = igRangesTjpList;
            igResultTubitak.IgTubitakRangesResults = igRangesTubitakList;
            var allDetails = new TestResultsFromManuals
            {
                Details = details,
                ManualApResults = igResultAp,
                ManualCilvResults = igResultCilv,
                ManualOsResults = igResultOs,
                ManualTjpResults = igResultTjp,
                ManualTubitakResults = igResultTubitak
            };
            return allDetails;
        }


        private static string GetRangeResult(float? value, float lowerLimit, float upperLimit)
        {
            if (value < lowerLimit) return "Dusuk";
            if (value > upperLimit) return "Yuksek";
            return "Normal";
        }


    }
}
