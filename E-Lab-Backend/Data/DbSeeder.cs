using AutoMapper;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Interface;
using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Data
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DbSeeder> _logger;
        public DbSeeder(ApplicationDbContext context, IPasswordHasher passwordHasher, IUserRepository userRepository,
            IMapper mapper, ILogger<DbSeeder> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SeedDataContext()
        {
            try
            {
                if (!_context.Users.Any())
                {
                    await SeedAdmins();
                    await SeedPatients();
                }

                if (!_context.TestResults.Any())
                    await SeedTestResults();

                if (!_context.IgsManualTjp.Any())
                    await SeedIgManualTjp();

                if (!_context.IgsManualOs.Any())
                    await SeedIgManualOs();

                if (!_context.IgsManualCilvPrimer.Any())
                    await SeedIgManualCilvPrimer();

                if (!_context.IgsManualCilvSeconder.Any())
                    await SeedIgManualCilvSec();

                if (!_context.IgsManualCilvSeconder.Any())
                    await SeedIgManualCilvSec();

                if (!_context.IgsManualAp.Any())
                    await SeedIgManualAp();

                if (!_context.IgsManualTubitak.Any())
                    await SeedIgManualTubitak();

                else
                    _logger.LogInformation("Database is already seeded");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error seeding data: {ex.Message}");
                _logger.LogError(ex.StackTrace);
            }
        }

        private async Task SeedAdmins()
        {
            string password = "admin123";
            string pwHashed = _passwordHasher.HashPassword(password);

            var admin = new UserModel
            {
                FullName = "Öner Özdemir",
                Tckn = "123456",
                PasswordHashed = pwHashed,
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-42)),
                Role = "Admin",
                Gender = GenderEnum.Female
            };
            await _context.Users.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
        private async Task SeedPatients()
        {
            var patients = new List<RegisterDto>
            {
                new RegisterDto { FullName = "M.A.", Tckn = "3790", Password = "mapw123", BirthDate = new DateOnly(2022,02,21), Gender=GenderEnum.Male },
                new RegisterDto { FullName = "M.E.C.", Tckn = "31*******02", Password = "mecpw123", BirthDate = new DateOnly(2021,03,24), Gender=GenderEnum.Male },
                new RegisterDto { FullName = "A.K.", Tckn = "53*******04", Password = "akpw123", BirthDate = new DateOnly(2019,12,20), Gender=GenderEnum.Female },
                new RegisterDto { FullName = "Y.K.", Tckn = "51*******46", Password = "ykpw123", BirthDate = new DateOnly(2022,05,10), Gender=GenderEnum.Female },
            };

            foreach (var patient in patients)
            {
                try
                {
                    var pwHashed = _passwordHasher.HashPassword(patient.Password);
                    var newUser = _mapper.Map<UserModel>(patient);
                    newUser.PasswordHashed = pwHashed;

                    await _userRepository.AddUser(newUser);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, $"Error registering patient with tckn: {patient.Tckn}");
                }
            }
        }

        private async Task SeedTestResults()
        {
            try
            {
                var userMa = await _userRepository.GetUserByTckn("3790") as SuccessDataResult<UserModel>;
                var userMec = await _userRepository.GetUserByTckn("31*******02") as SuccessDataResult<UserModel>;
                var userAk = await _userRepository.GetUserByTckn("53*******04") as SuccessDataResult<UserModel>;
                var userYk = await _userRepository.GetUserByTckn("51*******46") as SuccessDataResult<UserModel>;

                var testRes = new List<TestResult>
                {
                    new TestResult{PatientId=userMa.Data.Id, IgG=766 , IgA=49.4f , IgM=58, ExpertApproveTime= new DateTime(2024, 11, 07) },
                    new TestResult{PatientId=userMa.Data.Id, IgG=526 , IgA=49.4f , IgM=63, ExpertApproveTime= new DateTime(2024, 05, 09) },
                    new TestResult{PatientId=userMa.Data.Id, IgG=519 , IgA=49.4f , IgM=104, ExpertApproveTime= new DateTime(2023, 03, 30) },


                    new TestResult{PatientId=userMec.Data.Id, IgG=641 , IgA=43 , IgM=28, IgG1=418 , IgG2=254 , IgG3=18 , IgG4=9.4f, ExpertApproveTime= new DateTime(2024, 08, 12) },
                    new TestResult{PatientId=userMec.Data.Id, IgG=704 , IgA=null , IgM=23, IgG1=null , IgG2=null , IgG3=null , IgG4=null, ExpertApproveTime= new DateTime(2024, 02, 13) },
                    new TestResult{PatientId=userMec.Data.Id, IgG=865 , IgA=null , IgM=23, IgG1=null , IgG2=null , IgG3=null , IgG4=null, ExpertApproveTime= new DateTime(2023, 09, 12) },

                    new TestResult{PatientId=userAk.Data.Id, IgG=866 , IgA=74.8f , IgM=126, IgG1=546 , IgG2=199, IgG3=13, IgG4=6.9f, ExpertApproveTime= new DateTime(2024, 10, 07) },
                    new TestResult{PatientId=userAk.Data.Id, IgG=610 , IgA=75.6f , IgM=114, IgG1=392 , IgG2=135, IgG3=14, IgG4=3, ExpertApproveTime= new DateTime(2024, 07, 16) },
                    new TestResult{PatientId=userAk.Data.Id, IgG=697 , IgA=109 , IgM=131, IgG1=434 , IgG2=121, IgG3=14, IgG4=3.7f, ExpertApproveTime= new DateTime(2024, 07, 02) },

                    new TestResult{PatientId=userYk.Data.Id, IgG=636 , IgA=44.6f , IgM=83, IgG1=420 , IgG2=227 , IgG3=24 , IgG4=9.2f, ExpertApproveTime= new DateTime(2024, 08, 12) },
                    new TestResult{PatientId=userYk.Data.Id, IgG=630 , IgA=24.8f , IgM=82, IgG1=null , IgG2=null , IgG3=null , IgG4=null, ExpertApproveTime= new DateTime(2024, 02, 16) },
                    new TestResult{PatientId=userYk.Data.Id, IgG=475 , IgA=29 , IgM=105, IgG1=null , IgG2=null , IgG3=null , IgG4=null, ExpertApproveTime= new DateTime(2023, 11, 13) },

                };
                await _context.TestResults.AddRangeAsync(testRes);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Error registering test result.");
            }

        }

        private async Task SeedIgManualTubitak()
        {
            var manualIgG = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, IgType = IgTypeEnum.IgG, GeometricMean = 913.85f, GMStandardDeviation = 262.19f, Mean = 953f, MeanStandardDeviation = 262.19f, MinValue = 399f, MaxValue = 1480f, CILowerLimit = 855.1f, CIUpperLimit = 1050.9f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 3, IgType = IgTypeEnum.IgG, GeometricMean = 409.86f, GMStandardDeviation = 145.59f, Mean = 429.5f, MeanStandardDeviation = 145.59f, MinValue = 217f, MaxValue = 981f, CILowerLimit = 375.14f, CIUpperLimit = 483.86f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, IgType = IgTypeEnum.IgG, GeometricMean = 440.17f, GMStandardDeviation = 236.8f, Mean = 482.43f, MeanStandardDeviation = 236.8f, MinValue = 270f, MaxValue = 1110f, CILowerLimit = 394.01f, CIUpperLimit = 570.86f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, IgType = IgTypeEnum.IgG, GeometricMean = 536.79f, GMStandardDeviation = 186.62f, Mean = 568.97f, MeanStandardDeviation = 186.62f, MinValue = 242f, MaxValue = 977f, CILowerLimit = 499.28f, CIUpperLimit = 638.65f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, IgType = IgTypeEnum.IgG, GeometricMean = 726.79f, GMStandardDeviation = 238.61f, Mean = 761.7f, MeanStandardDeviation = 238.61f, MinValue = 389f, MaxValue = 1260f, CILowerLimit = 672.6f, CIUpperLimit = 850.8f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgG, GeometricMean = 786.41f, GMStandardDeviation = 249.14f, Mean = 811.5f, MeanStandardDeviation = 249.14f, MinValue = 486f, MaxValue = 1970f, CILowerLimit = 718.47f, CIUpperLimit = 904.53f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgG, GeometricMean = 823.19f, GMStandardDeviation = 164.19f, Mean = 839.87f, MeanStandardDeviation = 164.19f, MinValue = 457f, MaxValue = 1120f, CILowerLimit = 778.56f, CIUpperLimit = 901.18f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgG, GeometricMean = 982.86f, GMStandardDeviation = 255.53f, Mean = 1014.93f, MeanStandardDeviation = 255.53f, MinValue = 483f, MaxValue = 1580f, CILowerLimit = 919.52f, CIUpperLimit = 1110.35f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgG, GeometricMean = 1016.12f, GMStandardDeviation = 322.27f, Mean = 1055.43f, MeanStandardDeviation = 322.27f, MinValue = 642f, MaxValue = 2290f, CILowerLimit = 935.09f, CIUpperLimit = 1175.77f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgG, GeometricMean = 1123.56f, GMStandardDeviation = 203.83f, Mean = 1142.07f, MeanStandardDeviation = 203.83f, MinValue = 636f, MaxValue = 1610f, CILowerLimit = 1065.96f, CIUpperLimit = 1218.18f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgG, GeometricMean = 1277.2f, GMStandardDeviation = 361.89f, Mean = 1322.77f, MeanStandardDeviation = 361.89f, MinValue = 688f, MaxValue = 2430f, CILowerLimit = 1187.63f, CIUpperLimit = 1457.9f }
            };

            var igAList = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, IgType = IgTypeEnum.IgA, GeometricMean = 6.77f, GMStandardDeviation = 0.45f, Mean = 6.79f, MeanStandardDeviation = 0.45f, MinValue = 6.67f, MaxValue = 8.75f, CILowerLimit = 6.62f, CIUpperLimit = 6.95f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 3, IgType = IgTypeEnum.IgA, GeometricMean = 9.58f, GMStandardDeviation = 5.16f, Mean = 10.53f, MeanStandardDeviation = 5.16f, MinValue = 6.67f, MaxValue = 24.6f, CILowerLimit = 8.57f, CIUpperLimit = 12.49f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, IgType = IgTypeEnum.IgA, GeometricMean = 17.23f, GMStandardDeviation = 9.77f, Mean = 19.86f, MeanStandardDeviation = 9.77f, MinValue = 6.67f, MaxValue = 53f, CILowerLimit = 14.70f, CIUpperLimit = 25.01f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, IgType = IgTypeEnum.IgA, GeometricMean = 23.63f, GMStandardDeviation = 12.37f, Mean = 29.41f, MeanStandardDeviation = 12.37f, MinValue = 6.68f, MaxValue = 114f, CILowerLimit = 21.06f, CIUpperLimit = 37.77f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, IgType = IgTypeEnum.IgA, GeometricMean = 34.09f, GMStandardDeviation = 17.1f, Mean = 37.62f, MeanStandardDeviation = 17.1f, MinValue = 13.1f, MaxValue = 103f, CILowerLimit = 31.34f, CIUpperLimit = 47.85f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgA, GeometricMean = 48.87f, GMStandardDeviation = 24.52f, Mean = 59.77f, MeanStandardDeviation = 24.52f, MinValue = 6.67f, MaxValue = 135f, CILowerLimit = 46.05f, CIUpperLimit = 71.38f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgA, GeometricMean = 62.75f, GMStandardDeviation = 34.05f, Mean = 68.98f, MeanStandardDeviation = 34.05f, MinValue = 35.7f, MaxValue = 192f, CILowerLimit = 56.27f, CIUpperLimit = 81.7f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgA, GeometricMean = 97.38f, GMStandardDeviation = 49.66f, Mean = 106.9f, MeanStandardDeviation = 49.66f, MinValue = 44.8f, MaxValue = 276f, CILowerLimit = 88.36f, CIUpperLimit = 125.45f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgA, GeometricMean = 102.27f, GMStandardDeviation = 47.05f, Mean = 115.99f, MeanStandardDeviation = 47.05f, MinValue = 32.6f, MaxValue = 262f, CILowerLimit = 94.69f, CIUpperLimit = 137.29f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgA, GeometricMean = 112.16f, GMStandardDeviation = 47.51f, Mean = 120.90f, MeanStandardDeviation = 47.51f, MinValue = 36.4f, MaxValue = 305f, CILowerLimit = 99.29f, CIUpperLimit = 172.11f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgA, GeometricMean = 179.21f, GMStandardDeviation = 89.92f, Mean = 201.84f, MeanStandardDeviation = 89.92f, MinValue = 46.3f, MaxValue = 385f, CILowerLimit = 168.26f, CIUpperLimit = 235.41f }
            };


            var igMList = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, IgType = IgTypeEnum.IgM, GeometricMean = 16.89f, GMStandardDeviation = 8.87f, Mean = 20.38f, MeanStandardDeviation = 8.87f, MinValue = 5.1f, MaxValue = 50.9f, CILowerLimit = 15.57f, CIUpperLimit = 25.18f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 3, IgType = IgTypeEnum.IgM, GeometricMean = 34.21f, GMStandardDeviation = 13.55f, Mean = 36.66f, MeanStandardDeviation = 13.55f, MinValue = 15.2f, MaxValue = 68.5f, CILowerLimit = 31.60f, CIUpperLimit = 41.72f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, IgType = IgTypeEnum.IgM, GeometricMean = 69.05f, GMStandardDeviation = 29.73f, Mean = 75.44f, MeanStandardDeviation = 29.73f, MinValue = 26.9f, MaxValue = 130f, CILowerLimit = 64.34f, CIUpperLimit = 86.54f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, IgType = IgTypeEnum.IgM, GeometricMean = 73.42f, GMStandardDeviation = 35.76f, Mean = 81.05f, MeanStandardDeviation = 35.76f, MinValue = 24.2f, MaxValue = 162f, CILowerLimit = 67.7f, CIUpperLimit = 94.41f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, IgType = IgTypeEnum.IgM, GeometricMean = 115.25f, GMStandardDeviation = 41.63f, Mean = 122.57f, MeanStandardDeviation = 41.63f, MinValue = 38.6f, MaxValue = 195f, CILowerLimit = 107.03f, CIUpperLimit = 138.12f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgM, GeometricMean = 104.66f, GMStandardDeviation = 40.55f, Mean = 111.31f, MeanStandardDeviation = 40.55f, MinValue = 42.7f, MaxValue = 236f, CILowerLimit = 96.17f, CIUpperLimit = 126.46f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgM, GeometricMean = 115.60f, GMStandardDeviation = 39.24f, Mean = 121.79f, MeanStandardDeviation = 39.24f, MinValue = 58.7f, MaxValue = 198f, CILowerLimit = 107.13f, CIUpperLimit = 136.44f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgM, GeometricMean = 108.05f, GMStandardDeviation = 41.27f, Mean = 114.73f, MeanStandardDeviation = 41.27f, MinValue = 50.3f, MaxValue = 242f, CILowerLimit = 99.32f, CIUpperLimit = 130.14f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgM, GeometricMean = 119.16f, GMStandardDeviation = 39.31f, Mean = 125.78f, MeanStandardDeviation = 39.31f, MinValue = 42.4f, MaxValue = 197f, CILowerLimit = 111.1f, CIUpperLimit = 140.46f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgM, GeometricMean = 130.60f, GMStandardDeviation = 64.32f, Mean = 142.54f, MeanStandardDeviation = 64.32f, MinValue = 60.7f, MaxValue = 323f, CILowerLimit = 118.53f, CIUpperLimit = 166.55f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgM, GeometricMean = 130.60f, GMStandardDeviation = 64.32f, Mean = 142.54f, MeanStandardDeviation = 64.32f, MinValue = 60.7f, MaxValue = 323f, CILowerLimit = 118.53f, CIUpperLimit = 166.55f }
            };


            var igG1List = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgG1, GeometricMean = 510.74f, GMStandardDeviation = 192.04f, Mean = 531.7f, MeanStandardDeviation = 192.04f, MinValue = 309f, MaxValue = 1450f, CILowerLimit = 459.98f, CIUpperLimit = 603.41f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgG1, GeometricMean = 506.73f, GMStandardDeviation = 82.28f, Mean = 513.93f, MeanStandardDeviation = 82.28f, MinValue = 273f, MaxValue = 679f, CILowerLimit = 483.20f, CIUpperLimit = 544.65f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgG1, GeometricMean = 567.94f, GMStandardDeviation = 121.64f, Mean = 581f, MeanStandardDeviation = 121.64f, MinValue = 292f, MaxValue = 781f, CILowerLimit = 535.87f, CIUpperLimit = 626.72f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgG1, GeometricMean = 634.17f, GMStandardDeviation = 216.39f, Mean = 660.23f, MeanStandardDeviation = 216.39f, MinValue = 410f, MaxValue = 1530f, CILowerLimit = 579.43f, CIUpperLimit = 741.03f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgG1, GeometricMean = 635.52f, GMStandardDeviation = 131.01f, Mean = 648.53f, MeanStandardDeviation = 131.01f, MinValue = 344f, MaxValue = 958f, CILowerLimit = 599.61f, CIUpperLimit = 697.45f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgG1, GeometricMean = 645.35f, GMStandardDeviation = 229.62f, Mean = 674.5f, MeanStandardDeviation = 229.62f, MinValue = 403f, MaxValue = 1520f, CILowerLimit = 588.75f, CIUpperLimit = 760.24f }
            };


            var igG2List = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgG2, GeometricMean = 137.88f, GMStandardDeviation = 38.59f, Mean = 141.98f, MeanStandardDeviation = 38.59f, MinValue = 87.6f, MaxValue = 289f, CILowerLimit = 127.57f, CIUpperLimit = 156.39f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgG2, GeometricMean = 143.92f, GMStandardDeviation = 50.80f, Mean = 151.95f, MeanStandardDeviation = 50.80f, MinValue = 73.3f, MaxValue = 271f, CILowerLimit = 132.98f, CIUpperLimit = 170.92f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgG2, GeometricMean = 196.57f, GMStandardDeviation = 86.41f, Mean = 213.67f, MeanStandardDeviation = 86.41f, MinValue = 88.1f, MaxValue = 408f, CILowerLimit = 181.40f, CIUpperLimit = 245.93f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgG2, GeometricMean = 250.67f, GMStandardDeviation = 85.72f, Mean = 265.56f, MeanStandardDeviation = 85.72f, MinValue = 81f, MaxValue = 442f, CILowerLimit = 233.55f, CIUpperLimit = 297.57f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgG2, GeometricMean = 261.62f, GMStandardDeviation = 69.14f, Mean = 270.23f, MeanStandardDeviation = 69.14f, MinValue = 159f, MaxValue = 406f, CILowerLimit = 244.41f, CIUpperLimit = 296.05f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgG2, GeometricMean = 359.76f, GMStandardDeviation = 115.83f, Mean = 375.9f, MeanStandardDeviation = 115.83f, MinValue = 184f, MaxValue = 696f, CILowerLimit = 332.6f, CIUpperLimit = 419.15f }
            };

            var igG3List = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgG3, GeometricMean = 48.78f, GMStandardDeviation = 16.90f, Mean = 51.73f, MeanStandardDeviation = 16.90f, MinValue = 19.8f, MaxValue = 75f, CILowerLimit = 45.41f, CIUpperLimit = 58.04f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgG3, GeometricMean = 44.05f, GMStandardDeviation = 21.55f, Mean = 45.26f, MeanStandardDeviation = 21.55f, MinValue = 20.8f, MaxValue = 93.2f, CILowerLimit = 37.21f, CIUpperLimit = 53.30f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgG3, GeometricMean = 56.82f, GMStandardDeviation = 30.55f, Mean = 65.53f, MeanStandardDeviation = 30.55f, MinValue = 18.9f, MaxValue = 135f, CILowerLimit = 53.01f, CIUpperLimit = 78.06f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgG3, GeometricMean = 77.59f, GMStandardDeviation = 37.37f, Mean = 84.19f, MeanStandardDeviation = 37.37f, MinValue = 34.1f, MaxValue = 200f, CILowerLimit = 70.24f, CIUpperLimit = 98.15f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgG3, GeometricMean = 75.30f, GMStandardDeviation = 31.86f, Mean = 81.39f, MeanStandardDeviation = 31.86f, MinValue = 35.2f, MaxValue = 150f, CILowerLimit = 69.49f, CIUpperLimit = 93.28f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgG3, GeometricMean = 86.33f, GMStandardDeviation = 43.29f, Mean = 95.12f, MeanStandardDeviation = 43.29f, MinValue = 29.3f, MaxValue = 200f, CILowerLimit = 78.95f, CIUpperLimit = 111.28f }
            };

            var igG4List = new List<IgManualTubitak>
            {
                new IgManualTubitak { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, IgType = IgTypeEnum.IgG4, GeometricMean = 15.53f, GMStandardDeviation = 8.54f, Mean = 18.37f, MeanStandardDeviation = 8.54f, MinValue = 7.86f, MaxValue = 57.5f, CILowerLimit = 13.67f, CIUpperLimit = 23.07f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, IgType = IgTypeEnum.IgG4, GeometricMean = 30.81f, GMStandardDeviation = 15.42f, Mean = 40.75f, MeanStandardDeviation = 15.42f, MinValue = 7.86f, MaxValue = 122f, CILowerLimit = 28.84f, CIUpperLimit = 52.65f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, IgType = IgTypeEnum.IgG4, GeometricMean = 39.33f, GMStandardDeviation = 23.05f, Mean = 50.94f, MeanStandardDeviation = 23.05f, MinValue = 7.86f, MaxValue = 157f, CILowerLimit = 37.11f, CIUpperLimit = 64.77f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, IgType = IgTypeEnum.IgG4, GeometricMean = 25.36f, GMStandardDeviation = 15.39f, Mean = 35.51f, MeanStandardDeviation = 15.39f, MinValue = 7.86f, MaxValue = 93.8f, CILowerLimit = 24.95f, CIUpperLimit = 46.07f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, IgType = IgTypeEnum.IgG4, GeometricMean = 31.03f, GMStandardDeviation = 16.73f, Mean = 39.51f, MeanStandardDeviation = 16.73f, MinValue = 7.86f, MaxValue = 119f, CILowerLimit = 29.53f, CIUpperLimit = 49.49f },
                new IgManualTubitak { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, IgType = IgTypeEnum.IgG4, GeometricMean = 38.89f, GMStandardDeviation = 23.08f, Mean = 50.16f, MeanStandardDeviation = 23.08f, MinValue = 7.86f, MaxValue = 157f, CILowerLimit = 36.32f, CIUpperLimit = 64.01f }
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.IgsManualTubitak.AddRangeAsync(igAList);
                await _context.IgsManualTubitak.AddRangeAsync(manualIgG);
                await _context.IgsManualTubitak.AddRangeAsync(igMList);
                await _context.IgsManualTubitak.AddRangeAsync(igG1List);
                await _context.IgsManualTubitak.AddRangeAsync(igG2List);
                await _context.IgsManualTubitak.AddRangeAsync(igG3List);
                await _context.IgsManualTubitak.AddRangeAsync(igG4List);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }

        private async Task SeedIgManualAp()
        {
            var manual = new List<IgManualAp>
            {
                new IgManualAp{ AgeInMonthsLowerLimit= 0, AgeInMonthsUpperLimit= 5, IgGLowerLimit= 100, IgGUpperLimit= 134, IgG1LowerLimit= 56, IgG1UpperLimit= 215, IgG2LowerLimit= 0, IgG2UpperLimit= 82,
                IgG3LowerLimit= 7.6f, IgG3UpperLimit= 823, IgG4LowerLimit= 0, IgG4UpperLimit= 19.8f, IgALowerLimit= 7, IgAUpperLimit= 37, IgMLowerLimit= 26, IgMUpperLimit= 122 },

                new IgManualAp{ AgeInMonthsLowerLimit= 6, AgeInMonthsUpperLimit= 9, IgGLowerLimit= 164, IgGUpperLimit= 588, IgG1LowerLimit= 102, IgG1UpperLimit= 369, IgG2LowerLimit= 0, IgG2UpperLimit= 89,
                IgG3LowerLimit= 11.9f, IgG3UpperLimit= 74, IgG4LowerLimit= 0, IgG4UpperLimit= 19.8f, IgALowerLimit= 16, IgAUpperLimit= 50, IgMLowerLimit= 32, IgMUpperLimit= 132 },

                new IgManualAp{ AgeInMonthsLowerLimit= 10, AgeInMonthsUpperLimit= 15, IgGLowerLimit= 246, IgGUpperLimit= 904, IgG1LowerLimit= 160, IgG1UpperLimit= 562, IgG2LowerLimit= 24, IgG2UpperLimit= 98,
                IgG3LowerLimit= 17.3f, IgG3UpperLimit= 63.7f, IgG4LowerLimit= 0, IgG4UpperLimit= 22, IgALowerLimit= 27, IgAUpperLimit= 66, IgMLowerLimit= 40, IgMUpperLimit= 143 },

                new IgManualAp{ AgeInMonthsLowerLimit= 16, AgeInMonthsUpperLimit= 24, IgGLowerLimit= 313, IgGUpperLimit= 1170, IgG1LowerLimit= 209, IgG1UpperLimit= 724, IgG2LowerLimit= 35, IgG2UpperLimit= 105,
                IgG3LowerLimit= 21.9f, IgG3UpperLimit= 55.0f, IgG4LowerLimit= 0, IgG4UpperLimit= 23, IgALowerLimit= 36, IgAUpperLimit= 79, IgMLowerLimit= 46, IgMUpperLimit= 152 },

                new IgManualAp{ AgeInMonthsLowerLimit= 25, AgeInMonthsUpperLimit= 48, IgGLowerLimit= 295, IgGUpperLimit= 1156, IgG1LowerLimit= 158, IgG1UpperLimit= 721, IgG2LowerLimit= 39, IgG2UpperLimit= 176,
                IgG3LowerLimit= 17, IgG3UpperLimit= 84.7f, IgG4LowerLimit= 0.4f, IgG4UpperLimit= 49.1f, IgALowerLimit= 27, IgAUpperLimit= 246, IgMLowerLimit= 37, IgMUpperLimit= 184 },

                new IgManualAp{ AgeInMonthsLowerLimit= 49, AgeInMonthsUpperLimit= 84, IgGLowerLimit= 386, IgGUpperLimit= 1470, IgG1LowerLimit= 209, IgG1UpperLimit= 902, IgG2LowerLimit= 44, IgG2UpperLimit= 316,
                IgG3LowerLimit= 10.8f, IgG3UpperLimit= 94.9f, IgG4LowerLimit= 0.8f, IgG4UpperLimit= 81.9f, IgALowerLimit= 29, IgAUpperLimit= 256, IgMLowerLimit= 37, IgMUpperLimit= 224 },

                new IgManualAp{ AgeInMonthsLowerLimit= 85, AgeInMonthsUpperLimit= 120, IgGLowerLimit= 462, IgGUpperLimit= 1682, IgG1LowerLimit= 253, IgG1UpperLimit= 1019, IgG2LowerLimit= 54, IgG2UpperLimit= 435,
                IgG3LowerLimit= 8.5f, IgG3UpperLimit= 1026, IgG4LowerLimit= 1, IgG4UpperLimit= 108.7f, IgALowerLimit= 34, IgAUpperLimit= 274, IgMLowerLimit= 38, IgMUpperLimit= 251 },

                new IgManualAp{ AgeInMonthsLowerLimit= 121, AgeInMonthsUpperLimit= 156, IgGLowerLimit= 503, IgGUpperLimit= 1580, IgG1LowerLimit= 280, IgG1UpperLimit= 1030, IgG2LowerLimit= 66, IgG2UpperLimit= 502,
                IgG3LowerLimit= 11.5f, IgG3UpperLimit= 1053, IgG4LowerLimit= 1, IgG4UpperLimit= 121.9f, IgALowerLimit= 42, IgAUpperLimit= 295, IgMLowerLimit= 41, IgMUpperLimit= 255 },

                new IgManualAp{ AgeInMonthsLowerLimit= 157, AgeInMonthsUpperLimit= 192, IgGLowerLimit= 509, IgGUpperLimit= 1580, IgG1LowerLimit= 289, IgG1UpperLimit= 934, IgG2LowerLimit= 82, IgG2UpperLimit= 516,
                IgG3LowerLimit= 20, IgG3UpperLimit= 1032, IgG4LowerLimit= 0.7f, IgG4UpperLimit= 121.7f, IgALowerLimit= 52, IgAUpperLimit= 319, IgMLowerLimit= 45, IgMUpperLimit= 244 },

                new IgManualAp{ AgeInMonthsLowerLimit= 193, AgeInMonthsUpperLimit= 216, IgGLowerLimit= 487, IgGUpperLimit= 1327, IgG1LowerLimit= 283, IgG1UpperLimit= 772, IgG2LowerLimit= 98, IgG2UpperLimit= 486,
                IgG3LowerLimit= 31.3f, IgG3UpperLimit= 97.6f, IgG4LowerLimit= 0.3f, IgG4UpperLimit= 111, IgALowerLimit= 60, IgAUpperLimit= 337, IgMLowerLimit= 49, IgMUpperLimit= 201 },

                new IgManualAp{ AgeInMonthsLowerLimit= 217, AgeInMonthsUpperLimit= 1200, IgGLowerLimit= 767, IgGUpperLimit= 1590, IgG1LowerLimit= 341, IgG1UpperLimit= 894, IgG2LowerLimit= 171, IgG2UpperLimit= 632,
                IgG3LowerLimit= 18.4f, IgG3UpperLimit= 1060, IgG4LowerLimit= 2.4f, IgG4UpperLimit= 121, IgALowerLimit= 61, IgAUpperLimit= 356, IgMLowerLimit= 37, IgMUpperLimit= 286 }
            };
            await _context.IgsManualAp.AddRangeAsync(manual);
            await _context.SaveChangesAsync();
        }
        private async Task SeedIgManualCilvPrimer()
        {
            var manual = new List<IgManualCilvPrimer>
            {
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 0, AgeInMonthsUpperLimit= 1, IgGLowerLimit= 700, IgGUpperLimit= 1300, IgALowerLimit= 0, IgAUpperLimit= 11, IgMLowerLimit= 5, IgMUpperLimit= 30},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 2, AgeInMonthsUpperLimit= 4, IgGLowerLimit= 280, IgGUpperLimit= 750, IgALowerLimit= 6, IgAUpperLimit= 50, IgMLowerLimit= 15, IgMUpperLimit= 70},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 5, AgeInMonthsUpperLimit= 7, IgGLowerLimit= 200, IgGUpperLimit= 1200, IgALowerLimit= 8, IgAUpperLimit= 90, IgMLowerLimit= 10, IgMUpperLimit= 90},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 8, AgeInMonthsUpperLimit= 13, IgGLowerLimit= 300, IgGUpperLimit= 1500, IgALowerLimit= 16, IgAUpperLimit= 100, IgMLowerLimit= 25, IgMUpperLimit= 115},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 14, AgeInMonthsUpperLimit= 36, IgGLowerLimit= 400, IgGUpperLimit= 1300, IgALowerLimit= 20, IgAUpperLimit= 230, IgMLowerLimit= 30, IgMUpperLimit= 120},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 37, AgeInMonthsUpperLimit= 72, IgGLowerLimit= 600, IgGUpperLimit= 1500, IgALowerLimit= 50, IgAUpperLimit= 150, IgMLowerLimit= 22, IgMUpperLimit= 100},
                new IgManualCilvPrimer { AgeInMonthsLowerLimit= 73, AgeInMonthsUpperLimit= 216, IgGLowerLimit= 639, IgGUpperLimit= 1344, IgALowerLimit= 70, IgAUpperLimit= 312, IgMLowerLimit= 56, IgMUpperLimit= 352}
            };
            await _context.IgsManualCilvPrimer.AddRangeAsync(manual);
            await _context.SaveChangesAsync();
        }
        private async Task SeedIgManualCilvSec()
        {
            var manual = new List<IgManualCilvSeconder>
            {
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= -9, AgeInMonthsUpperLimit= 0, IgG1LowerLimit= 435, IgG1UpperLimit= 1084, IgG2LowerLimit= 143, IgG2UpperLimit= 453, IgG3LowerLimit= 27, IgG3UpperLimit= 146, IgG4LowerLimit= 1, IgG4UpperLimit= 47 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 1, AgeInMonthsUpperLimit= 3, IgG1LowerLimit= 218, IgG1UpperLimit= 496, IgG2LowerLimit= 40, IgG2UpperLimit= 167, IgG3LowerLimit= 4, IgG3UpperLimit= 23, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 4, AgeInMonthsUpperLimit= 6, IgG1LowerLimit= 143, IgG1UpperLimit= 394, IgG2LowerLimit= 23, IgG2UpperLimit= 147, IgG3LowerLimit= 4, IgG3UpperLimit= 100, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 7, AgeInMonthsUpperLimit= 9, IgG1LowerLimit= 190, IgG1UpperLimit= 388, IgG2LowerLimit= 37, IgG2UpperLimit= 60, IgG3LowerLimit= 12, IgG3UpperLimit= 62, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 10, AgeInMonthsUpperLimit= 24, IgG1LowerLimit= 286, IgG1UpperLimit= 680, IgG2LowerLimit= 30, IgG2UpperLimit= 327, IgG3LowerLimit= 13, IgG3UpperLimit= 82, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 25, AgeInMonthsUpperLimit= 48, IgG1LowerLimit= 381, IgG1UpperLimit= 884, IgG2LowerLimit= 70, IgG2UpperLimit= 443, IgG3LowerLimit= 17, IgG3UpperLimit= 90, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 49, AgeInMonthsUpperLimit= 72, IgG1LowerLimit= 292, IgG1UpperLimit= 816, IgG2LowerLimit= 83, IgG2UpperLimit= 513, IgG3LowerLimit= 8, IgG3UpperLimit= 111, IgG4LowerLimit= 2, IgG4UpperLimit= 112 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 73, AgeInMonthsUpperLimit= 96, IgG1LowerLimit= 422, IgG1UpperLimit= 802, IgG2LowerLimit= 113, IgG2UpperLimit= 480, IgG3LowerLimit= 15, IgG3UpperLimit= 133, IgG4LowerLimit= 1, IgG4UpperLimit= 138 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 97, AgeInMonthsUpperLimit= 120, IgG1LowerLimit= 456, IgG1UpperLimit= 938, IgG2LowerLimit= 163, IgG2UpperLimit= 513, IgG3LowerLimit= 26, IgG3UpperLimit= 113, IgG4LowerLimit= 1, IgG4UpperLimit= 95 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 121, AgeInMonthsUpperLimit= 144, IgG1LowerLimit= 456, IgG1UpperLimit= 952, IgG2LowerLimit= 147, IgG2UpperLimit= 493, IgG3LowerLimit= 12, IgG3UpperLimit= 179, IgG4LowerLimit= 1, IgG4UpperLimit= 153 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 145, AgeInMonthsUpperLimit= 168, IgG1LowerLimit= 347, IgG1UpperLimit= 993, IgG2LowerLimit= 140, IgG2UpperLimit= 440, IgG3LowerLimit= 23, IgG3UpperLimit= 117, IgG4LowerLimit= 1, IgG4UpperLimit= 143 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 169, AgeInMonthsUpperLimit= 216, IgG1LowerLimit= 422, IgG1UpperLimit= 1292, IgG2LowerLimit= 117, IgG2UpperLimit= 747, IgG3LowerLimit= 41, IgG3UpperLimit= 129, IgG4LowerLimit= 10, IgG4UpperLimit= 67 }
            };
            await _context.IgsManualCilvSeconder.AddRangeAsync(manual);
            await _context.SaveChangesAsync();
        }
        private async Task SeedIgManualOs()
        {
            var manualIgG = new List<IgManualOs>
            {
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 491.2f, AMStandardDeviation = 156.57f, MinValue = 227f, MaxValue = 770f, PValue = 0.058f },
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 375.9f, AMStandardDeviation = 88.25f, MinValue = 236f, MaxValue = 505f, PValue = 0.058f },

                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 354.7f, AMStandardDeviation = 147.93f, MinValue = 141f, MaxValue = 691f, PValue = 0.493f },
                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 409.6f, AMStandardDeviation = 199.39f, MinValue = 145f, MaxValue = 885f, PValue = 0.493f },

                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 626.6f, AMStandardDeviation = 197.47f, MinValue = 379f, MaxValue = 1010f, PValue = 0.856f },
                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 609.6f, AMStandardDeviation = 214.36f, MinValue = 350f, MaxValue = 951f, PValue = 0.856f },

                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 688.4f, AMStandardDeviation = 179.88f, MinValue = 475f, MaxValue = 959f, PValue = 0.676f },
                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 653.8f, AMStandardDeviation = 183.74f, MinValue = 432f, MaxValue = 990f, PValue = 0.676f },

                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 908.8f, AMStandardDeviation = 185.3f, MinValue = 726f, MaxValue = 1320f, PValue = 0.087f },
                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 760.7f, AMStandardDeviation = 180.39f, MinValue = 437f, MaxValue = 1130f, PValue = 0.087f },

                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1175f, AMStandardDeviation = 159.98f, MinValue = 849f, MaxValue = 1400f, PValue = 0.001f },
                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 813.8f, AMStandardDeviation = 142.09f, MinValue = 524f, MaxValue = 1020f, PValue = 0.001f },

                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1279.4f, AMStandardDeviation = 234.59f, MinValue = 884f, MaxValue = 1600f, PValue = 0.017f },
                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1046.1f, AMStandardDeviation = 155.95f, MinValue = 858f, MaxValue = 1350f, PValue = 0.017f },

                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1219.7f, AMStandardDeviation = 216.05f, MinValue = 763f, MaxValue = 1490f, PValue = 0.458f },
                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1134.5f, AMStandardDeviation = 281.72f, MinValue = 645f, MaxValue = 1520f, PValue = 0.458f },

                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1278.7f, AMStandardDeviation = 205.3f, MinValue = 924f, MaxValue = 1620f, PValue = 0.216f },
                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1149.2f, AMStandardDeviation = 244.65f, MinValue = 877f, MaxValue = 1560f, PValue = 0.216f },

                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1413.3f, AMStandardDeviation = 355.14f, MinValue = 863f, MaxValue = 1830f, PValue = 0.587f },
                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1330.6f, AMStandardDeviation = 312.43f, MinValue = 758f, MaxValue = 1760f, PValue = 0.587f }
            };

            var manualIgA = new List<IgManualOs>
            {
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 12.1f, AMStandardDeviation = 8.44f, MinValue = 6f, MaxValue = 33f, PValue = 0.161f },
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 19.8f, AMStandardDeviation = 14.17f, MinValue = 7f, MaxValue = 47f, PValue = 0.161f },

                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 16.4f, AMStandardDeviation = 8.59f, MinValue = 7f, MaxValue = 28f, PValue = 0.220f },
                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 23.5f, AMStandardDeviation = 15.45f, MinValue = 8f, MaxValue = 63f, PValue = 0.220f },

                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 41.7f, AMStandardDeviation = 30.73f, MinValue = 14f, MaxValue = 114f, PValue = 0.273f },
                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 29.8f, AMStandardDeviation = 11.03f, MinValue = 12f, MaxValue = 52f, PValue = 0.273f },

                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 36.2f, AMStandardDeviation = 11.82f, MinValue = 15f, MaxValue = 52f, PValue = 0.651f },
                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 38.5f, AMStandardDeviation = 10.5f, MinValue = 21f, MaxValue = 52f, PValue = 0.651f },

                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 56.5f, AMStandardDeviation = 10.19f, MinValue = 43f, MaxValue = 71f, PValue = 0.536f },
                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 51.9f, AMStandardDeviation = 20.68f, MinValue = 24f, MaxValue = 84f, PValue = 0.536f },

                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 104.1f, AMStandardDeviation = 19.34f, MinValue = 79f, MaxValue = 135f, PValue = 0.033f },
                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 84.6f, AMStandardDeviation = 18.37f, MinValue = 55f, MaxValue = 106f, PValue = 0.033f },

                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 148.4f, AMStandardDeviation = 57.34f, MinValue = 82f, MaxValue = 264f, PValue = 0.087f },
                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 112.1f, AMStandardDeviation = 26.94f, MinValue = 81f, MaxValue = 150f, PValue = 0.087f },

                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 176.7f, AMStandardDeviation = 87.17f, MinValue = 82f, MaxValue = 334f, PValue = 0.316f },
                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 144.2f, AMStandardDeviation = 48.22f, MinValue = 78f, MaxValue = 233f, PValue = 0.316f },

                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 163.7f, AMStandardDeviation = 55.77f, MinValue = 88f, MaxValue = 234f, PValue = 0.993f },
                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 163.5f, AMStandardDeviation = 45.61f, MinValue = 87f, MaxValue = 230f, PValue = 0.993f },

                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 192.9f, AMStandardDeviation = 95.77f, MinValue = 6f, MaxValue = 359f, PValue = 0.489f },
                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 219.7f, AMStandardDeviation = 72.48f, MinValue = 111f, MaxValue = 350f, PValue = 0.489f }
            };
            var manualIgM = new List<IgManualOs>
            {
                // 1-3 Ay
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 38.5f, AMStandardDeviation = 21.51f, MinValue = 19f, MaxValue = 87f, PValue = 0.315f },
                new IgManualOs { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 3, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 30.9f, AMStandardDeviation = 8.88f, MinValue = 18f, MaxValue = 46f, PValue = 0.315f },

                // 4-6 Ay
                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 39.1f, AMStandardDeviation = 12.8f, MinValue = 18f, MaxValue = 61f, PValue = 0.052f },
                new IgManualOs { AgeInMonthsLowerLimit = 4, AgeInMonthsUpperLimit = 6, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 61.2f, AMStandardDeviation = 30.99f, MinValue = 32f, MaxValue = 136f, PValue = 0.052f },

                // 7-12 Ay
                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 58.2f, AMStandardDeviation = 20.37f, MinValue = 29f, MaxValue = 95f, PValue = 0.960f },
                new IgManualOs { AgeInMonthsLowerLimit = 7, AgeInMonthsUpperLimit = 12, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 57.7f, AMStandardDeviation = 23.2f, MinValue = 28f, MaxValue = 115f, PValue = 0.960f },

                // 13-24 Ay
                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 75.1f, AMStandardDeviation = 33.2f, MinValue = 42f, MaxValue = 148f, PValue = 0.216f },
                new IgManualOs { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 95.7f, AMStandardDeviation = 38.47f, MinValue = 32f, MaxValue = 146f, PValue = 0.216f },

                // 25-36 Ay
                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 105f, AMStandardDeviation = 24.77f, MinValue = 64f, MaxValue = 144f, PValue = 0.441f },
                new IgManualOs { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 95.9f, AMStandardDeviation = 26.86f, MinValue = 47f, MaxValue = 137f, PValue = 0.441f },

                // 4-5 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 119.7f, AMStandardDeviation = 33.79f, MinValue = 65f, MaxValue = 179f, PValue = 0.872f },
                new IgManualOs { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 122.5f, AMStandardDeviation = 42.38f, MinValue = 68f, MaxValue = 205f, PValue = 0.872f },

                // 6-8 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 121.9f, AMStandardDeviation = 44.24f, MinValue = 47f, MaxValue = 198f, PValue = 0.683f },
                new IgManualOs { AgeInMonthsLowerLimit = 61, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 114.4f, AMStandardDeviation = 36.1f, MinValue = 53f, MaxValue = 175f, PValue = 0.683f },

                // 9-11 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 107.8f, AMStandardDeviation = 42.15f, MinValue = 54f, MaxValue = 163f, PValue = 0.508f },
                new IgManualOs { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 96.6f, AMStandardDeviation = 31.3f, MinValue = 38f, MaxValue = 150f, PValue = 0.508f },

                // 12-16 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 124.4f, AMStandardDeviation = 49.3f, MinValue = 47f, MaxValue = 180f, PValue = 0.141f },
                new IgManualOs { AgeInMonthsLowerLimit = 133, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 162.8f, AMStandardDeviation = 61.48f, MinValue = 57f, MaxValue = 285f, PValue = 0.141f },

                // 17-18 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 138.3f, AMStandardDeviation = 58.63f, MinValue = 64f, MaxValue = 256f, PValue = 0.397f },
                new IgManualOs { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 118.7f, AMStandardDeviation = 40.86f, MinValue = 61f, MaxValue = 180f, PValue = 0.397f }
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.IgsManualOs.AddRangeAsync(manualIgA);
                await _context.IgsManualOs.AddRangeAsync(manualIgG);
                await _context.IgsManualOs.AddRangeAsync(manualIgM);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }
        private async Task SeedIgManualTjp()
        {
            var manualIgG = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 16, IgType = IgTypeEnum.IgG, GeometricMean = 884.2f, GMStandardDeviation = 230.4f, MinValue = 492, MaxValue = 1190, CILowerLimit = 792.0f, CIUpperLimit = 1037.5f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5, NumOfSubjects = 12, IgType = IgTypeEnum.IgG, GeometricMean = 473.8f, GMStandardDeviation = 193.1f, MinValue = 270, MaxValue = 792, CILowerLimit = 384.2f, CIUpperLimit = 629.7f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 18, IgType = IgTypeEnum.IgG, GeometricMean = 581.9f, GMStandardDeviation = 207.9f, MinValue = 268, MaxValue = 898, CILowerLimit = 515.6f, CIUpperLimit = 722.4f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 26, IgType = IgTypeEnum.IgG, GeometricMean = 692.7f, GMStandardDeviation = 181.1f, MinValue = 421, MaxValue = 1100, CILowerLimit = 641.9f, CIUpperLimit = 788.2f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 60, IgType = IgTypeEnum.IgG, GeometricMean = 774.4f, GMStandardDeviation = 199.7f, MinValue = 365, MaxValue = 1200, CILowerLimit = 748.2f, CIUpperLimit = 851.4f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 52, IgType = IgTypeEnum.IgG, GeometricMean = 822.3f, GMStandardDeviation = 208.4f, MinValue = 430, MaxValue = 1290, CILowerLimit = 790.4f, CIUpperLimit = 906.4f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 40, IgType = IgTypeEnum.IgG, GeometricMean = 879.9f, GMStandardDeviation = 157.2f, MinValue = 539, MaxValue = 1200, CILowerLimit = 844.1f, CIUpperLimit = 944.6f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 70, IgType = IgTypeEnum.IgG, GeometricMean = 986.2f, GMStandardDeviation = 209.6f, MinValue = 528, MaxValue = 1490, CILowerLimit = 958.5f, CIUpperLimit = 1058.5f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 66, IgType = IgTypeEnum.IgG, GeometricMean = 1040.7f, GMStandardDeviation = 203.2f, MinValue = 527, MaxValue = 1590, CILowerLimit = 1011.5f, CIUpperLimit = 1111.4f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 57, IgType = IgTypeEnum.IgG, GeometricMean = 1062.8f, GMStandardDeviation = 238.8f, MinValue = 646, MaxValue = 1620, CILowerLimit = 1024.9f, CIUpperLimit = 1151.7f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 34, IgType = IgTypeEnum.IgG, GeometricMean = 1051.7f, GMStandardDeviation = 228.9f, MinValue = 579, MaxValue = 1610, CILowerLimit = 995.9f, CIUpperLimit = 1155.6f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 25, IgType = IgTypeEnum.IgG, GeometricMean = 1087.8f, GMStandardDeviation = 236.0f, MinValue = 741, MaxValue = 1650, CILowerLimit = 1014.2f, CIUpperLimit = 1209.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 17, IgType = IgTypeEnum.IgG, GeometricMean = 981.1f, GMStandardDeviation = 207.7f, MinValue = 666, MaxValue = 1370, CILowerLimit = 895.3f, CIUpperLimit = 1108.9f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 17, IgType = IgTypeEnum.IgG, GeometricMean = 1224.9f, GMStandardDeviation = 280.2f, MinValue = 830, MaxValue = 1820, CILowerLimit = 1109.9f, CIUpperLimit = 1398.0f }
           };

            var manualIgA = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 16, IgType = IgTypeEnum.IgA, GeometricMean = 5.7f, GMStandardDeviation = 0.2f, MinValue = 5.0f, MaxValue = 5.8f, CILowerLimit = 5.6f, CIUpperLimit = 5.9f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5, NumOfSubjects = 12, IgType = IgTypeEnum.IgA, GeometricMean = 20.2f, GMStandardDeviation = 19.7f, MinValue = 5.8f, MaxValue = 58.0f, CILowerLimit = 15.8f, CIUpperLimit = 40.9f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 15, IgType = IgTypeEnum.IgA, GeometricMean = 23.2f, GMStandardDeviation = 25.2f, MinValue = 5.8f, MaxValue = 85.8f, CILowerLimit = 20.5f, CIUpperLimit = 48.5f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 26, IgType = IgTypeEnum.IgA, GeometricMean = 52.9f, GMStandardDeviation = 36.7f, MinValue = 18.4f, MaxValue = 154.0f, CILowerLimit = 47.2f, CIUpperLimit = 76.9f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 57, IgType = IgTypeEnum.IgA, GeometricMean = 44.1f, GMStandardDeviation = 18.3f, MinValue = 11.5f, MaxValue = 94.3f, CILowerLimit = 42.9f, CIUpperLimit = 52.6f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 52, IgType = IgTypeEnum.IgA, GeometricMean = 53.5f, GMStandardDeviation = 26.8f, MinValue = 23.0f, MaxValue = 130.0f, CILowerLimit = 51.4f, CIUpperLimit = 66.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 39, IgType = IgTypeEnum.IgA, GeometricMean = 68.8f, GMStandardDeviation = 22.2f, MinValue = 40.7f, MaxValue = 115.0f, CILowerLimit = 64.8f, CIUpperLimit = 79.2f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 68, IgType = IgTypeEnum.IgA, GeometricMean = 91.9f, GMStandardDeviation = 37.4f, MinValue = 23.0f, MaxValue = 205.1f, CILowerLimit = 90.2f, CIUpperLimit = 108.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 64, IgType = IgTypeEnum.IgA, GeometricMean = 108.4f, GMStandardDeviation = 42.3f, MinValue = 36.1f, MaxValue = 268.0f, CILowerLimit = 105.9f, CIUpperLimit = 127.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 53, IgType = IgTypeEnum.IgA, GeometricMean = 116.7f, GMStandardDeviation = 45.9f, MinValue = 54.0f, MaxValue = 268.0f, CILowerLimit = 111.8f, CIUpperLimit = 137.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 31, IgType = IgTypeEnum.IgA, GeometricMean = 115.8f, GMStandardDeviation = 43.0f, MinValue = 27.0f, MaxValue = 198.0f, CILowerLimit = 109.7f, CIUpperLimit = 141.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 23, IgType = IgTypeEnum.IgA, GeometricMean = 130.5f, GMStandardDeviation = 47.4f, MinValue = 52.4f, MaxValue = 225.0f, CILowerLimit = 118.0f, CIUpperLimit = 159.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 15, IgType = IgTypeEnum.IgA, GeometricMean = 109.8f, GMStandardDeviation = 29.4f, MinValue = 48.0f, MaxValue = 158.0f, CILowerLimit = 97.8f, CIUpperLimit = 130.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 15, IgType = IgTypeEnum.IgA, GeometricMean = 121.3f, GMStandardDeviation = 55.5f, MinValue = 46.5f, MaxValue = 221.0f, CILowerLimit = 102.4f, CIUpperLimit = 163.8f },
            };

            var manualIgM = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 14, IgType = IgTypeEnum.IgM, GeometricMean = 18.5f, GMStandardDeviation = 3.5f, MinValue = 17.3f, MaxValue = 29.6f, CILowerLimit = 16.7f, CIUpperLimit = 20.7f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5, NumOfSubjects = 11, IgType = IgTypeEnum.IgM, GeometricMean = 57.3f, GMStandardDeviation = 37.4f, MinValue = 18.4f, MaxValue = 145.0f, CILowerLimit = 41.9f, CIUpperLimit = 92.1f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 17, IgType = IgTypeEnum.IgM, GeometricMean = 68.7f, GMStandardDeviation = 38.9f, MinValue = 26.4f, MaxValue = 146.0f, CILowerLimit = 58.5f, CIUpperLimit = 98.5f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 27, IgType = IgTypeEnum.IgM, GeometricMean = 86.1f, GMStandardDeviation = 40.3f, MinValue = 23.5f, MaxValue = 180.0f, CILowerLimit = 78.9f, CIUpperLimit = 110.8f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 57, IgType = IgTypeEnum.IgM, GeometricMean = 98.3f, GMStandardDeviation = 40.3f, MinValue = 25.6f, MaxValue = 201.0f, CILowerLimit = 96.3f, CIUpperLimit = 117.7f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 53, IgType = IgTypeEnum.IgM, GeometricMean = 92.5f, GMStandardDeviation = 33.9f, MinValue = 36.0f, MaxValue = 199.0f, CILowerLimit = 89.0f, CIUpperLimit = 107.7f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 38, IgType = IgTypeEnum.IgM, GeometricMean = 86.1f, GMStandardDeviation = 35.3f, MinValue = 26.1f, MaxValue = 188.0f, CILowerLimit = 80.9f, CIUpperLimit = 104.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 69, IgType = IgTypeEnum.IgM, GeometricMean = 105.8f, GMStandardDeviation = 40.8f, MinValue = 33.3f, MaxValue = 207.0f, CILowerLimit = 103.7f, CIUpperLimit = 123.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 65, IgType = IgTypeEnum.IgM, GeometricMean = 97.6f, GMStandardDeviation = 42.9f, MinValue = 30.5f, MaxValue = 220.0f, CILowerLimit = 95.5f, CIUpperLimit = 116.8f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 53, IgType = IgTypeEnum.IgM, GeometricMean = 93.9f, GMStandardDeviation = 49.3f, MinValue = 33.7f, MaxValue = 257.0f, CILowerLimit = 90.8f, CIUpperLimit = 118.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 32, IgType = IgTypeEnum.IgM, GeometricMean = 102.4f, GMStandardDeviation = 38.8f, MinValue = 30.0f, MaxValue = 187.0f, CILowerLimit = 96.0f, CIUpperLimit = 124.0f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 24, IgType = IgTypeEnum.IgM, GeometricMean = 120.9f, GMStandardDeviation = 43.8f, MinValue = 44.0f, MaxValue = 206.0f, CILowerLimit = 110.3f, CIUpperLimit = 147.3f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 15, IgType = IgTypeEnum.IgM, GeometricMean = 99.7f, GMStandardDeviation = 49.7f, MinValue = 33.0f, MaxValue = 205.0f, CILowerLimit = 83.7f, CIUpperLimit = 138.8f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 16, IgType = IgTypeEnum.IgM, GeometricMean = 130.9f, GMStandardDeviation = 44.5f, MinValue = 75.0f, MaxValue = 198.5f, CILowerLimit = 114.6f, CIUpperLimit = 161.9f }
            };

            var manualIgG1 = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 16, IgType = IgTypeEnum.IgG1, GeometricMean = 675f, GMStandardDeviation = 152f, MinValue = 430f, MaxValue = 897f, CILowerLimit = 611f, CIUpperLimit = 773f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5, NumOfSubjects = 11, IgType = IgTypeEnum.IgG1, GeometricMean = 319f, GMStandardDeviation = 113f, MinValue = 160f, MaxValue = 574f, CILowerLimit = 261f, CIUpperLimit = 413f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 14, IgType = IgTypeEnum.IgG1, GeometricMean = 485f, GMStandardDeviation = 188f, MinValue = 279f, MaxValue = 820f, CILowerLimit = 408f, CIUpperLimit = 625f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 29, IgType = IgTypeEnum.IgG1, GeometricMean = 562f, GMStandardDeviation = 240f, MinValue = 328f, MaxValue = 1250f, CILowerLimit = 506f, CIUpperLimit = 690f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 67, IgType = IgTypeEnum.IgG1, GeometricMean = 721f, GMStandardDeviation = 292f, MinValue = 344f, MaxValue = 1435f, CILowerLimit = 702f, CIUpperLimit = 844f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 60, IgType = IgTypeEnum.IgG1, GeometricMean = 736f, GMStandardDeviation = 285f, MinValue = 340f, MaxValue = 1470f, CILowerLimit = 712f, CIUpperLimit = 860f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 49, IgType = IgTypeEnum.IgG1, GeometricMean = 762f, GMStandardDeviation = 246f, MinValue = 439f, MaxValue = 1333f, CILowerLimit = 726f, CIUpperLimit = 867f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 58, IgType = IgTypeEnum.IgG1, GeometricMean = 755f, GMStandardDeviation = 209f, MinValue = 468f, MaxValue = 1333f, CILowerLimit = 726f, CIUpperLimit = 837f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 63, IgType = IgTypeEnum.IgG1, GeometricMean = 806f, GMStandardDeviation = 281f, MinValue = 420f, MaxValue = 1470f, CILowerLimit = 778f, CIUpperLimit = 920f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 66, IgType = IgTypeEnum.IgG1, GeometricMean = 860f, GMStandardDeviation = 329f, MinValue = 380f, MaxValue = 1840f, CILowerLimit = 834f, CIUpperLimit = 996f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 35, IgType = IgTypeEnum.IgG1, GeometricMean = 842f, GMStandardDeviation = 241f, MinValue = 599f, MaxValue = 1560f, CILowerLimit = 787f, CIUpperLimit = 953f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 32, IgType = IgTypeEnum.IgG1, GeometricMean = 872f, GMStandardDeviation = 354f, MinValue = 490f, MaxValue = 1560f, CILowerLimit = 805f, CIUpperLimit = 1061f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 21, IgType = IgTypeEnum.IgG1, GeometricMean = 796f, GMStandardDeviation = 269f, MinValue = 498f, MaxValue = 1460f, CILowerLimit = 711f, CIUpperLimit = 956f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 21, IgType = IgTypeEnum.IgG1, GeometricMean = 857f, GMStandardDeviation = 214f, MinValue = 528f, MaxValue = 1384f, CILowerLimit = 782f, CIUpperLimit = 978f }
            };

            var manualIgG2 = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 15, IgType = IgTypeEnum.IgG2, GeometricMean = 156f, GMStandardDeviation = 50f, MinValue = 87f, MaxValue = 263f, CILowerLimit = 135f, CIUpperLimit = 192f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5,  IgType = IgTypeEnum.IgG2, GeometricMean = 59f, GMStandardDeviation = 26f, MinValue = 32f, MaxValue = 108f, CILowerLimit = 46f, CIUpperLimit = 84f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 14, IgType = IgTypeEnum.IgG2, GeometricMean = 67f, GMStandardDeviation = 37f, MinValue = 36f, MaxValue = 146f, CILowerLimit = 53f, CIUpperLimit = 97f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 29, IgType = IgTypeEnum.IgG2, GeometricMean = 64f, GMStandardDeviation = 35f, MinValue = 25f, MaxValue = 161f, CILowerLimit = 58f, CIUpperLimit = 85f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 67, IgType = IgTypeEnum.IgG2, GeometricMean = 93f, GMStandardDeviation = 49f, MinValue = 31f, MaxValue = 264f, CILowerLimit = 92f, CIUpperLimit = 116f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 58, IgType = IgTypeEnum.IgG2, GeometricMean = 115f, GMStandardDeviation = 85f, MinValue = 43f, MaxValue = 380f, CILowerLimit = 112f, CIUpperLimit = 157f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 44, IgType = IgTypeEnum.IgG2, GeometricMean = 161f, GMStandardDeviation = 92f, MinValue = 60f, MaxValue = 410f, CILowerLimit = 155f, CIUpperLimit = 211f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 52, IgType = IgTypeEnum.IgG2, GeometricMean = 167f, GMStandardDeviation = 78f, MinValue = 85f, MaxValue = 440f, CILowerLimit = 160f, CIUpperLimit = 204f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 60, IgType = IgTypeEnum.IgG2, GeometricMean = 197f, GMStandardDeviation = 101f, MinValue = 67f, MaxValue = 460f, CILowerLimit = 193f, CIUpperLimit = 245f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 62, IgType = IgTypeEnum.IgG2, GeometricMean = 214f, GMStandardDeviation = 121f, MinValue = 70f, MaxValue = 543f, CILowerLimit = 211f, CIUpperLimit = 273f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 32, IgType = IgTypeEnum.IgG2, GeometricMean = 212f, GMStandardDeviation = 88f, MinValue = 111f, MaxValue = 515f, CILowerLimit = 195f, CIUpperLimit = 259f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 28, IgType = IgTypeEnum.IgG2, GeometricMean = 279f, GMStandardDeviation = 134f, MinValue = 100f, MaxValue = 573f, CILowerLimit = 257f, CIUpperLimit = 361f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 20, IgType = IgTypeEnum.IgG2, GeometricMean = 238f, GMStandardDeviation = 83f, MinValue = 110f, MaxValue = 398f, CILowerLimit = 214f, CIUpperLimit = 292f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 20, IgType = IgTypeEnum.IgG2, GeometricMean = 307f, GMStandardDeviation = 128f, MinValue = 147f, MaxValue = 610f, CILowerLimit = 271f, CIUpperLimit = 391f }
            };

            var manualIgG3 = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 15, IgType = IgTypeEnum.IgG3, GeometricMean = 37f, GMStandardDeviation = 17f, MinValue = 18f, MaxValue = 78f, CILowerLimit = 31f, CIUpperLimit = 50f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5,  IgType = IgTypeEnum.IgG3, GeometricMean = 24f, GMStandardDeviation = 12f, MinValue = 13f, MaxValue = 53f, CILowerLimit = 17f, CIUpperLimit = 35f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 14, IgType = IgTypeEnum.IgG3, GeometricMean = 35f, GMStandardDeviation = 25f, MinValue = 14f, MaxValue = 100f, CILowerLimit = 27f, CIUpperLimit = 56f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 29, IgType = IgTypeEnum.IgG3, GeometricMean = 38f, GMStandardDeviation = 24f, MinValue = 18f, MaxValue = 110f, CILowerLimit = 34f, CIUpperLimit = 53f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 65, IgType = IgTypeEnum.IgG3, GeometricMean = 37f, GMStandardDeviation = 25f, MinValue = 16f, MaxValue = 132f, CILowerLimit = 37f, CIUpperLimit = 49f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 54, IgType = IgTypeEnum.IgG3, GeometricMean = 32f, GMStandardDeviation = 21f, MinValue = 14f, MaxValue = 125f, CILowerLimit = 30f, CIUpperLimit = 42f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 45, IgType = IgTypeEnum.IgG3, GeometricMean = 37f, GMStandardDeviation = 25f, MinValue = 15f, MaxValue = 120f, CILowerLimit = 35f, CIUpperLimit = 50f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 54, IgType = IgTypeEnum.IgG3, GeometricMean = 37f, GMStandardDeviation = 20f, MinValue = 15f, MaxValue = 107f, CILowerLimit = 36f, CIUpperLimit = 47f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 62, IgType = IgTypeEnum.IgG3, GeometricMean = 51f, GMStandardDeviation = 43f, MinValue = 21f, MaxValue = 186f, CILowerLimit = 51f, CIUpperLimit = 73f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 65, IgType = IgTypeEnum.IgG3, GeometricMean = 51f, GMStandardDeviation = 34f, MinValue = 20f, MaxValue = 186f, CILowerLimit = 50f, CIUpperLimit = 67f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 34, IgType = IgTypeEnum.IgG3, GeometricMean = 53f, GMStandardDeviation = 40f, MinValue = 29f, MaxValue = 200f, CILowerLimit = 47f, CIUpperLimit = 75f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 29, IgType = IgTypeEnum.IgG3, GeometricMean = 80f, GMStandardDeviation = 56f, MinValue = 28f, MaxValue = 223f, CILowerLimit = 73f, CIUpperLimit = 117f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 18, IgType = IgTypeEnum.IgG3, GeometricMean = 58f, GMStandardDeviation = 21f, MinValue = 30f, MaxValue = 120f, CILowerLimit = 51f, CIUpperLimit = 73f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 21, IgType = IgTypeEnum.IgG3, GeometricMean = 50f, GMStandardDeviation = 33f, MinValue = 21f, MaxValue = 152f, CILowerLimit = 43f, CIUpperLimit = 73f }
            };

            var manualIgG4 = new List<IgManualTjp>
            {
                new IgManualTjp { AgeInMonthsLowerLimit = 0, AgeInMonthsUpperLimit = 1, NumOfSubjects = 15, IgType = IgTypeEnum.IgG4, GeometricMean = 24f, GMStandardDeviation = 17f, MinValue = 17f, MaxValue = 81f, CILowerLimit = 17f, CIUpperLimit = 36f },
                new IgManualTjp { AgeInMonthsLowerLimit = 2, AgeInMonthsUpperLimit = 5,  IgType = IgTypeEnum.IgG4, GeometricMean = 15f, GMStandardDeviation = 14f, MinValue = 2f, MaxValue = 48f, CILowerLimit = 10f, CIUpperLimit = 31f },
                new IgManualTjp { AgeInMonthsLowerLimit = 6, AgeInMonthsUpperLimit = 8, NumOfSubjects = 15, IgType = IgTypeEnum.IgG4, GeometricMean = 14f, GMStandardDeviation = 11f, MinValue = 2f, MaxValue = 52f, CILowerLimit = 12f, CIUpperLimit = 25f },
                new IgManualTjp { AgeInMonthsLowerLimit = 9, AgeInMonthsUpperLimit = 12, NumOfSubjects = 27, IgType = IgTypeEnum.IgG4, GeometricMean = 12f, GMStandardDeviation = 5f, MinValue = 2f, MaxValue = 20f, CILowerLimit = 12f, CIUpperLimit = 16f },
                new IgManualTjp { AgeInMonthsLowerLimit = 13, AgeInMonthsUpperLimit = 24, NumOfSubjects = 69, IgType = IgTypeEnum.IgG4, GeometricMean = 16f, GMStandardDeviation = 17f, MinValue = 2f, MaxValue = 99f, CILowerLimit = 18f, CIUpperLimit = 26f },
                new IgManualTjp { AgeInMonthsLowerLimit = 25, AgeInMonthsUpperLimit = 36, NumOfSubjects = 61, IgType = IgTypeEnum.IgG4, GeometricMean = 20f, GMStandardDeviation = 40f, MinValue = 2f, MaxValue = 171f, CILowerLimit = 23f, CIUpperLimit = 43f },
                new IgManualTjp { AgeInMonthsLowerLimit = 37, AgeInMonthsUpperLimit = 48, NumOfSubjects = 50, IgType = IgTypeEnum.IgG4, GeometricMean = 27f, GMStandardDeviation = 37f, MinValue = 4f, MaxValue = 185f, CILowerLimit = 27f, CIUpperLimit = 48f },
                new IgManualTjp { AgeInMonthsLowerLimit = 49, AgeInMonthsUpperLimit = 72, NumOfSubjects = 56, IgType = IgTypeEnum.IgG4, GeometricMean = 35f, GMStandardDeviation = 46f, MinValue = 8f, MaxValue = 227f, CILowerLimit = 37f, CIUpperLimit = 62f },
                new IgManualTjp { AgeInMonthsLowerLimit = 73, AgeInMonthsUpperLimit = 96, NumOfSubjects = 64, IgType = IgTypeEnum.IgG4, GeometricMean = 42f, GMStandardDeviation = 46f, MinValue = 2f, MaxValue = 198f, CILowerLimit = 49f, CIUpperLimit = 72f },
                new IgManualTjp { AgeInMonthsLowerLimit = 97, AgeInMonthsUpperLimit = 120, NumOfSubjects = 69, IgType = IgTypeEnum.IgG4, GeometricMean = 36f, GMStandardDeviation = 45f, MinValue = 5f, MaxValue = 202f, CILowerLimit = 41f, CIUpperLimit = 63f },
                new IgManualTjp { AgeInMonthsLowerLimit = 121, AgeInMonthsUpperLimit = 144, NumOfSubjects = 35, IgType = IgTypeEnum.IgG4, GeometricMean = 34f, GMStandardDeviation = 44f, MinValue = 4f, MaxValue = 160f, CILowerLimit = 34f, CIUpperLimit = 64f },
                new IgManualTjp { AgeInMonthsLowerLimit = 145, AgeInMonthsUpperLimit = 168, NumOfSubjects = 31, IgType = IgTypeEnum.IgG4, GeometricMean = 51f, GMStandardDeviation = 45f, MinValue = 10f, MaxValue = 144f, CILowerLimit = 51f, CIUpperLimit = 84f },
                new IgManualTjp { AgeInMonthsLowerLimit = 169, AgeInMonthsUpperLimit = 192, NumOfSubjects = 20, IgType = IgTypeEnum.IgG4, GeometricMean = 36f, GMStandardDeviation = 44f, MinValue = 9f, MaxValue = 187f, CILowerLimit = 30f, CIUpperLimit = 72f },
                new IgManualTjp { AgeInMonthsLowerLimit = 193, AgeInMonthsUpperLimit = 216, NumOfSubjects = 23, IgType = IgTypeEnum.IgG4, GeometricMean = 33f, GMStandardDeviation = 47f, MinValue = 15f, MaxValue = 202f, CILowerLimit = 25f, CIUpperLimit = 66f }
            };

            using var transaction = await _context.Database.BeginTransactionAsync();        // atomicity
            try
            {
                await _context.IgsManualTjp.AddRangeAsync(manualIgA);
                await _context.IgsManualTjp.AddRangeAsync(manualIgG);
                await _context.IgsManualTjp.AddRangeAsync(manualIgM);
                await _context.IgsManualTjp.AddRangeAsync(manualIgG1);
                await _context.IgsManualTjp.AddRangeAsync(manualIgG2);
                await _context.IgsManualTjp.AddRangeAsync(manualIgG3);
                await _context.IgsManualTjp.AddRangeAsync(manualIgG4);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error during seeding: {ex.Message}");
            }
        }

    }
}
