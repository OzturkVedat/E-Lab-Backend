﻿// <auto-generated />
using System;
using E_Lab_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Lab_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualAp", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("IgALowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgAUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG1LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG1UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG2LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG2UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG3LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG3UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG4LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG4UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgGLowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgGUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgMLowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgMUpperLimit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("IgsManualAp");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualCilvPrimer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("IgALowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgAUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgGLowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgGUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgMLowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgMUpperLimit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("IgsManualCilvPrimer");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualCilvSeconder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("IgG1LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG1UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG2LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG2UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG3LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG3UpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG4LowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("IgG4UpperLimit")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("IgsManualCilvSeconder");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualOs", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("AMStandardDeviation")
                        .HasColumnType("real");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("ArithmeticMean")
                        .HasColumnType("real");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("IgType")
                        .HasColumnType("int");

                    b.Property<float>("MaxValue")
                        .HasColumnType("real");

                    b.Property<float>("MinValue")
                        .HasColumnType("real");

                    b.Property<int>("NumOfSubjects")
                        .HasColumnType("int");

                    b.Property<float>("PValue")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("IgsManualOs");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualTjp", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("CILowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("CIUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("GMStandardDeviation")
                        .HasColumnType("real");

                    b.Property<float>("GeometricMean")
                        .HasColumnType("real");

                    b.Property<int>("IgType")
                        .HasColumnType("int");

                    b.Property<float>("MaxValue")
                        .HasColumnType("real");

                    b.Property<float>("MinValue")
                        .HasColumnType("real");

                    b.Property<int>("NumOfSubjects")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IgsManualTjp");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.IgManualTubitak", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AgeInMonthsLowerLimit")
                        .HasColumnType("int");

                    b.Property<int>("AgeInMonthsUpperLimit")
                        .HasColumnType("int");

                    b.Property<float>("CILowerLimit")
                        .HasColumnType("real");

                    b.Property<float>("CIUpperLimit")
                        .HasColumnType("real");

                    b.Property<float>("GMStandardDeviation")
                        .HasColumnType("real");

                    b.Property<float>("GeometricMean")
                        .HasColumnType("real");

                    b.Property<int>("IgType")
                        .HasColumnType("int");

                    b.Property<float>("MaxValue")
                        .HasColumnType("real");

                    b.Property<float>("Mean")
                        .HasColumnType("real");

                    b.Property<float>("MeanStandardDeviation")
                        .HasColumnType("real");

                    b.Property<float>("MinValue")
                        .HasColumnType("real");

                    b.Property<int>("NumOfSubjects")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IgsManualTubitak");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.TestResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpertApproveTime")
                        .HasColumnType("datetime2");

                    b.Property<float?>("IgA")
                        .HasColumnType("real");

                    b.Property<float?>("IgG")
                        .HasColumnType("real");

                    b.Property<float?>("IgG1")
                        .HasColumnType("real");

                    b.Property<float?>("IgG2")
                        .HasColumnType("real");

                    b.Property<float?>("IgG3")
                        .HasColumnType("real");

                    b.Property<float?>("IgG4")
                        .HasColumnType("real");

                    b.Property<float?>("IgM")
                        .HasColumnType("real");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SampleAcceptTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SampleCollectionTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SampleType")
                        .HasColumnType("int");

                    b.Property<DateTime>("TestRequestTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tckn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Tckn")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.RefreshToken", b =>
                {
                    b.HasOne("E_Lab_Backend.Models.UserModel", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("E_Lab_Backend.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.TestResult", b =>
                {
                    b.HasOne("E_Lab_Backend.Models.UserModel", "Patient")
                        .WithMany("TestResults")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("E_Lab_Backend.Models.UserModel", b =>
                {
                    b.Navigation("RefreshToken");

                    b.Navigation("TestResults");
                });
#pragma warning restore 612, 618
        }
    }
}
