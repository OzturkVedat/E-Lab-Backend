using E_Lab_Backend.Models;
using E_Lab_Backend.Services;
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
                if(!_context.Users.Any())
                {
                    await SeedAdmins();
                    await SeedPatients();
                }
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
            string pwHashed= _passwordHasher.HashPassword(password);

            var admin = new UserModel
            {
                FullName = "Seeded Admin",
                Email = "admin@example.com",
                PasswordHashed = pwHashed,
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Role="Admin"
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
    }
}
