using E_Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Lab_Backend.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TestResultPatient> TestResultsPatient { get; set; }
        public DbSet<TestResultAdmin> TestResultsAdmin { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TestResultPatient>()
                .HasOne(t => t.Patient)
                .WithMany(p => p.PatientTestResults)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserModel>()
                .HasOne(u => u.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<RefreshToken>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // delete the tokens when the user is deleted
        }
    }
}
