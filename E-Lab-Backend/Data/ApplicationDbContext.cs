﻿using E_Lab_Backend.Models;
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
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<IgManualOs> IgsManualOs {  get; set; }
        public DbSet<IgManualTjp> IgsManualTjp { get; set; }
        public DbSet<IgManualCilvPrimer> IgsManualCilvPrimer { get; set; }
        public DbSet<IgManualCilvSeconder> IgsManualCilvSeconder { get; set; }
        public DbSet<IgManualAp> IgsManualAp { get; set; }
        public DbSet<IgManualTubitak> IgsManualTubitak { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserModel>()
                .HasOne(u => u.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<RefreshToken>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // delete the tokens when the user is deleted

            builder.Entity<UserModel>()     // indexing the email column
                .HasIndex(u => u.Email)
                .IsUnique();

        }
    }
}
