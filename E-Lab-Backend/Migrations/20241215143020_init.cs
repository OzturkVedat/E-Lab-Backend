using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Lab_Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IgsManualAp",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    IgGLowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgGUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgALowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgAUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgMLowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgMUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG1LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG1UpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG2LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG2UpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG3LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG3GUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG4LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG4UpperLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualAp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgsManualCilvPrimer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    IgGLowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgGUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgALowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgAUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgMLowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgMUpperLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualCilvPrimer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgsManualCilvSeconder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    IgG1LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG1UpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG2LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG2UpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG3LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG3GUpperLimit = table.Column<float>(type: "real", nullable: false),
                    IgG4LowerLimit = table.Column<float>(type: "real", nullable: false),
                    IgG4UpperLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualCilvSeconder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgsManualOs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    NumOfSubjects = table.Column<int>(type: "int", nullable: false),
                    IgType = table.Column<int>(type: "int", nullable: false),
                    ArithmeticMean = table.Column<float>(type: "real", nullable: false),
                    AMStandardDeviation = table.Column<float>(type: "real", nullable: false),
                    MinValue = table.Column<float>(type: "real", nullable: false),
                    MaxValue = table.Column<float>(type: "real", nullable: false),
                    PValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgsManualTjp",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    NumOfSubjects = table.Column<int>(type: "int", nullable: false),
                    IgType = table.Column<int>(type: "int", nullable: false),
                    GeometricMean = table.Column<float>(type: "real", nullable: false),
                    GMStandardDeviation = table.Column<float>(type: "real", nullable: false),
                    MinValue = table.Column<float>(type: "real", nullable: false),
                    MaxValue = table.Column<float>(type: "real", nullable: false),
                    CIUpperLimit = table.Column<float>(type: "real", nullable: false),
                    CILowerLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualTjp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IgsManualTubitak",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgeInMonthsUpperLimit = table.Column<int>(type: "int", nullable: false),
                    AgeInMonthsLowerLimit = table.Column<int>(type: "int", nullable: false),
                    NumOfSubjects = table.Column<int>(type: "int", nullable: false),
                    IgType = table.Column<int>(type: "int", nullable: false),
                    GeometricMean = table.Column<float>(type: "real", nullable: false),
                    GMStandardDeviation = table.Column<float>(type: "real", nullable: false),
                    Mean = table.Column<float>(type: "real", nullable: false),
                    MeanStandardDeviation = table.Column<float>(type: "real", nullable: false),
                    MinValue = table.Column<float>(type: "real", nullable: false),
                    MaxValue = table.Column<float>(type: "real", nullable: false),
                    CIUpperLimit = table.Column<float>(type: "real", nullable: false),
                    CILowerLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgsManualTubitak", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IgsManualAp");

            migrationBuilder.DropTable(
                name: "IgsManualCilvPrimer");

            migrationBuilder.DropTable(
                name: "IgsManualCilvSeconder");

            migrationBuilder.DropTable(
                name: "IgsManualOs");

            migrationBuilder.DropTable(
                name: "IgsManualTjp");

            migrationBuilder.DropTable(
                name: "IgsManualTubitak");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
