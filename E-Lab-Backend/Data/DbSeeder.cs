using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Data
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<DbSeeder> _logger;
        public DbSeeder(ApplicationDbContext context, IPasswordHasher passwordHasher, ILogger<DbSeeder> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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
                FullName = "Seeded Admin",
                Email = "admin@example.com",
                PasswordHashed = pwHashed,
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Role = "Admin"
            };
            await _context.Users.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
        private async Task SeedPatients()
        {
            string password = "patient123";
            string pwHashed = _passwordHasher.HashPassword(password);

            var admin = new UserModel
            {
                FullName = "Seeded Patient",
                Email = "patient@example.com",
                PasswordHashed = pwHashed,
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Role = "User"
            };
            await _context.Users.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        private async Task SeedIgManualTubitak()
        {
            var manual = new List<IgManualTubitak>
            {
                new IgManualTubitak{ age }

            };
        }

        private async Task SeedIgManualAp()
        {
            var manual = new List<IgManualAp>
            {
                new IgManualAp{ AgeInMonthsLowerLimit= 0, AgeInMonthsUpperLimit= 5, IgGLowerLimit= 100, IgGUpperLimit= 134, IgG1LowerLimit= 56, IgG1UpperLimit= 215, IgG2LowerLimit= 0, IgG2UpperLimit= 82,
                IgG3LowerLimit= 7.6f, IgG3GUpperLimit= 823, IgG4LowerLimit= 0, IgG4UpperLimit= 19.8f, IgALowerLimit= 7, IgAUpperLimit= 37, IgMLowerLimit= 26, IgMUpperLimit= 122 },

                new IgManualAp{ AgeInMonthsLowerLimit= 6, AgeInMonthsUpperLimit= 9, IgGLowerLimit= 164, IgGUpperLimit= 588, IgG1LowerLimit= 102, IgG1UpperLimit= 369, IgG2LowerLimit= 0, IgG2UpperLimit= 89,
                IgG3LowerLimit= 11.9f, IgG3GUpperLimit= 74, IgG4LowerLimit= 0, IgG4UpperLimit= 19.8f, IgALowerLimit= 16, IgAUpperLimit= 50, IgMLowerLimit= 32, IgMUpperLimit= 132 },

                new IgManualAp{ AgeInMonthsLowerLimit= 10, AgeInMonthsUpperLimit= 15, IgGLowerLimit= 246, IgGUpperLimit= 904, IgG1LowerLimit= 160, IgG1UpperLimit= 562, IgG2LowerLimit= 24, IgG2UpperLimit= 98,
                IgG3LowerLimit= 17.3f, IgG3GUpperLimit= 63.7f, IgG4LowerLimit= 0, IgG4UpperLimit= 22, IgALowerLimit= 27, IgAUpperLimit= 66, IgMLowerLimit= 40, IgMUpperLimit= 143 },

                new IgManualAp{ AgeInMonthsLowerLimit= 16, AgeInMonthsUpperLimit= 24, IgGLowerLimit= 313, IgGUpperLimit= 1170, IgG1LowerLimit= 209, IgG1UpperLimit= 724, IgG2LowerLimit= 35, IgG2UpperLimit= 105,
                IgG3LowerLimit= 21.9f, IgG3GUpperLimit= 55.0f, IgG4LowerLimit= 0, IgG4UpperLimit= 23, IgALowerLimit= 36, IgAUpperLimit= 79, IgMLowerLimit= 46, IgMUpperLimit= 152 },

                new IgManualAp{ AgeInMonthsLowerLimit= 25, AgeInMonthsUpperLimit= 48, IgGLowerLimit= 295, IgGUpperLimit= 1156, IgG1LowerLimit= 158, IgG1UpperLimit= 721, IgG2LowerLimit= 39, IgG2UpperLimit= 176,
                IgG3LowerLimit= 17, IgG3GUpperLimit= 84.7f, IgG4LowerLimit= 0.4f, IgG4UpperLimit= 49.1f, IgALowerLimit= 27, IgAUpperLimit= 246, IgMLowerLimit= 37, IgMUpperLimit= 184 },

                new IgManualAp{ AgeInMonthsLowerLimit= 49, AgeInMonthsUpperLimit= 84, IgGLowerLimit= 386, IgGUpperLimit= 1470, IgG1LowerLimit= 209, IgG1UpperLimit= 902, IgG2LowerLimit= 44, IgG2UpperLimit= 316,
                IgG3LowerLimit= 10.8f, IgG3GUpperLimit= 94.9f, IgG4LowerLimit= 0.8f, IgG4UpperLimit= 81.9f, IgALowerLimit= 29, IgAUpperLimit= 256, IgMLowerLimit= 37, IgMUpperLimit= 224 },

                new IgManualAp{ AgeInMonthsLowerLimit= 85, AgeInMonthsUpperLimit= 120, IgGLowerLimit= 462, IgGUpperLimit= 1682, IgG1LowerLimit= 253, IgG1UpperLimit= 1019, IgG2LowerLimit= 54, IgG2UpperLimit= 435,
                IgG3LowerLimit= 8.5f, IgG3GUpperLimit= 1026, IgG4LowerLimit= 1, IgG4UpperLimit= 108.7f, IgALowerLimit= 34, IgAUpperLimit= 274, IgMLowerLimit= 38, IgMUpperLimit= 251 },

                new IgManualAp{ AgeInMonthsLowerLimit= 121, AgeInMonthsUpperLimit= 156, IgGLowerLimit= 503, IgGUpperLimit= 1580, IgG1LowerLimit= 280, IgG1UpperLimit= 1030, IgG2LowerLimit= 66, IgG2UpperLimit= 502,
                IgG3LowerLimit= 11.5f, IgG3GUpperLimit= 1053, IgG4LowerLimit= 1, IgG4UpperLimit= 121.9f, IgALowerLimit= 42, IgAUpperLimit= 295, IgMLowerLimit= 41, IgMUpperLimit= 255 },

                new IgManualAp{ AgeInMonthsLowerLimit= 157, AgeInMonthsUpperLimit= 192, IgGLowerLimit= 509, IgGUpperLimit= 1580, IgG1LowerLimit= 289, IgG1UpperLimit= 934, IgG2LowerLimit= 82, IgG2UpperLimit= 516,
                IgG3LowerLimit= 20, IgG3GUpperLimit= 1032, IgG4LowerLimit= 0.7f, IgG4UpperLimit= 121.7f, IgALowerLimit= 52, IgAUpperLimit= 319, IgMLowerLimit= 45, IgMUpperLimit= 244 },

                new IgManualAp{ AgeInMonthsLowerLimit= 193, AgeInMonthsUpperLimit= 216, IgGLowerLimit= 487, IgGUpperLimit= 1327, IgG1LowerLimit= 283, IgG1UpperLimit= 772, IgG2LowerLimit= 98, IgG2UpperLimit= 486,
                IgG3LowerLimit= 31.3f, IgG3GUpperLimit= 97.6f, IgG4LowerLimit= 0.3f, IgG4UpperLimit= 111, IgALowerLimit= 60, IgAUpperLimit= 337, IgMLowerLimit= 49, IgMUpperLimit= 201 },

                new IgManualAp{ AgeInMonthsLowerLimit= 217, AgeInMonthsUpperLimit= 1200, IgGLowerLimit= 767, IgGUpperLimit= 1590, IgG1LowerLimit= 341, IgG1UpperLimit= 894, IgG2LowerLimit= 171, IgG2UpperLimit= 632,
                IgG3LowerLimit= 18.4f, IgG3GUpperLimit= 1060, IgG4LowerLimit= 2.4f, IgG4UpperLimit= 121, IgALowerLimit= 61, IgAUpperLimit= 356, IgMLowerLimit= 37, IgMUpperLimit= 286 }
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
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= -9, AgeInMonthsUpperLimit= 0, IgG1LowerLimit= 435, IgG1UpperLimit= 1084, IgG2LowerLimit= 143, IgG2UpperLimit= 453, IgG3LowerLimit= 27, IgG3GUpperLimit= 146, IgG4LowerLimit= 1, IgG4UpperLimit= 47 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 1, AgeInMonthsUpperLimit= 3, IgG1LowerLimit= 218, IgG1UpperLimit= 496, IgG2LowerLimit= 40, IgG2UpperLimit= 167, IgG3LowerLimit= 4, IgG3GUpperLimit= 23, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 4, AgeInMonthsUpperLimit= 6, IgG1LowerLimit= 143, IgG1UpperLimit= 394, IgG2LowerLimit= 23, IgG2UpperLimit= 147, IgG3LowerLimit= 4, IgG3GUpperLimit= 100, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 7, AgeInMonthsUpperLimit= 9, IgG1LowerLimit= 190, IgG1UpperLimit= 388, IgG2LowerLimit= 37, IgG2UpperLimit= 60, IgG3LowerLimit= 12, IgG3GUpperLimit= 62, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 10, AgeInMonthsUpperLimit= 24, IgG1LowerLimit= 286, IgG1UpperLimit= 680, IgG2LowerLimit= 30, IgG2UpperLimit= 327, IgG3LowerLimit= 13, IgG3GUpperLimit= 82, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 25, AgeInMonthsUpperLimit= 48, IgG1LowerLimit= 381, IgG1UpperLimit= 884, IgG2LowerLimit= 70, IgG2UpperLimit= 443, IgG3LowerLimit= 17, IgG3GUpperLimit= 90, IgG4LowerLimit= 1, IgG4UpperLimit= 120 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 49, AgeInMonthsUpperLimit= 72, IgG1LowerLimit= 292, IgG1UpperLimit= 816, IgG2LowerLimit= 83, IgG2UpperLimit= 513, IgG3LowerLimit= 8, IgG3GUpperLimit= 111, IgG4LowerLimit= 2, IgG4UpperLimit= 112 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 73, AgeInMonthsUpperLimit= 96, IgG1LowerLimit= 422, IgG1UpperLimit= 802, IgG2LowerLimit= 113, IgG2UpperLimit= 480, IgG3LowerLimit= 15, IgG3GUpperLimit= 133, IgG4LowerLimit= 1, IgG4UpperLimit= 138 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 97, AgeInMonthsUpperLimit= 120, IgG1LowerLimit= 456, IgG1UpperLimit= 938, IgG2LowerLimit= 163, IgG2UpperLimit= 513, IgG3LowerLimit= 26, IgG3GUpperLimit= 113, IgG4LowerLimit= 1, IgG4UpperLimit= 95 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 121, AgeInMonthsUpperLimit= 144, IgG1LowerLimit= 456, IgG1UpperLimit= 952, IgG2LowerLimit= 147, IgG2UpperLimit= 493, IgG3LowerLimit= 12, IgG3GUpperLimit= 179, IgG4LowerLimit= 1, IgG4UpperLimit= 153 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 145, AgeInMonthsUpperLimit= 168, IgG1LowerLimit= 347, IgG1UpperLimit= 993, IgG2LowerLimit= 140, IgG2UpperLimit= 440, IgG3LowerLimit= 23, IgG3GUpperLimit= 117, IgG4LowerLimit= 1, IgG4UpperLimit= 143 },
                new IgManualCilvSeconder { AgeInMonthsLowerLimit= 169, AgeInMonthsUpperLimit= 216, IgG1LowerLimit= 422, IgG1UpperLimit= 1292, IgG2LowerLimit= 117, IgG2UpperLimit= 747, IgG3LowerLimit= 41, IgG3GUpperLimit= 129, IgG4LowerLimit= 10, IgG4UpperLimit= 67 }
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

                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1175f, AMStandardDeviation = 159.98f, MinValue = 849f, MaxValue = 1400f, PValue = 0.001f },
                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 813.8f, AMStandardDeviation = 142.09f, MinValue = 524f, MaxValue = 1020f, PValue = 0.001f },

                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1279.4f, AMStandardDeviation = 234.59f, MinValue = 884f, MaxValue = 1600f, PValue = 0.017f },
                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1046.1f, AMStandardDeviation = 155.95f, MinValue = 858f, MaxValue = 1350f, PValue = 0.017f },

                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1219.7f, AMStandardDeviation = 216.05f, MinValue = 763f, MaxValue = 1490f, PValue = 0.458f },
                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1134.5f, AMStandardDeviation = 281.72f, MinValue = 645f, MaxValue = 1520f, PValue = 0.458f },

                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1278.7f, AMStandardDeviation = 205.3f, MinValue = 924f, MaxValue = 1620f, PValue = 0.216f },
                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1149.2f, AMStandardDeviation = 244.65f, MinValue = 877f, MaxValue = 1560f, PValue = 0.216f },

                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1413.3f, AMStandardDeviation = 355.14f, MinValue = 863f, MaxValue = 1830f, PValue = 0.587f },
                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female,  IgType = IgTypeEnum.IgG, ArithmeticMean = 1330.6f, AMStandardDeviation = 312.43f, MinValue = 758f, MaxValue = 1760f, PValue = 0.587f }
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

                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 104.1f, AMStandardDeviation = 19.34f, MinValue = 79f, MaxValue = 135f, PValue = 0.033f },
                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 84.6f, AMStandardDeviation = 18.37f, MinValue = 55f, MaxValue = 106f, PValue = 0.033f },

                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 148.4f, AMStandardDeviation = 57.34f, MinValue = 82f, MaxValue = 264f, PValue = 0.087f },
                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 112.1f, AMStandardDeviation = 26.94f, MinValue = 81f, MaxValue = 150f, PValue = 0.087f },

                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 176.7f, AMStandardDeviation = 87.17f, MinValue = 82f, MaxValue = 334f, PValue = 0.316f },
                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 144.2f, AMStandardDeviation = 48.22f, MinValue = 78f, MaxValue = 233f, PValue = 0.316f },

                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 163.7f, AMStandardDeviation = 55.77f, MinValue = 88f, MaxValue = 234f, PValue = 0.993f },
                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 163.5f, AMStandardDeviation = 45.61f, MinValue = 87f, MaxValue = 230f, PValue = 0.993f },

                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgA, ArithmeticMean = 192.9f, AMStandardDeviation = 95.77f, MinValue = 6f, MaxValue = 359f, PValue = 0.489f },
                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgA, ArithmeticMean = 219.7f, AMStandardDeviation = 72.48f, MinValue = 111f, MaxValue = 350f, PValue = 0.489f }
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
                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 119.7f, AMStandardDeviation = 33.79f, MinValue = 65f, MaxValue = 179f, PValue = 0.872f },
                new IgManualOs { AgeInMonthsLowerLimit = 48, AgeInMonthsUpperLimit = 60, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 122.5f, AMStandardDeviation = 42.38f, MinValue = 68f, MaxValue = 205f, PValue = 0.872f },

                // 6-8 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 121.9f, AMStandardDeviation = 44.24f, MinValue = 47f, MaxValue = 198f, PValue = 0.683f },
                new IgManualOs { AgeInMonthsLowerLimit = 72, AgeInMonthsUpperLimit = 96, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 114.4f, AMStandardDeviation = 36.1f, MinValue = 53f, MaxValue = 175f, PValue = 0.683f },

                // 9-11 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 107.8f, AMStandardDeviation = 42.15f, MinValue = 54f, MaxValue = 163f, PValue = 0.508f },
                new IgManualOs { AgeInMonthsLowerLimit = 108, AgeInMonthsUpperLimit = 132, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 96.6f, AMStandardDeviation = 31.3f, MinValue = 38f, MaxValue = 150f, PValue = 0.508f },

                // 12-16 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 124.4f, AMStandardDeviation = 49.3f, MinValue = 47f, MaxValue = 180f, PValue = 0.141f },
                new IgManualOs { AgeInMonthsLowerLimit = 144, AgeInMonthsUpperLimit = 192, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 162.8f, AMStandardDeviation = 61.48f, MinValue = 57f, MaxValue = 285f, PValue = 0.141f },

                // 17-18 Yaş
                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Male, IgType = IgTypeEnum.IgM, ArithmeticMean = 138.3f, AMStandardDeviation = 58.63f, MinValue = 64f, MaxValue = 256f, PValue = 0.397f },
                new IgManualOs { AgeInMonthsLowerLimit = 204, AgeInMonthsUpperLimit = 216, Gender = GenderEnum.Female, IgType = IgTypeEnum.IgM, ArithmeticMean = 118.7f, AMStandardDeviation = 40.86f, MinValue = 61f, MaxValue = 180f, PValue = 0.397f }
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
