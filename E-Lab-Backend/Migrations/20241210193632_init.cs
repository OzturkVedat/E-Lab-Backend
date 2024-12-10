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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TestResultsAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IgA = table.Column<float>(type: "real", nullable: false),
                    IgAResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgM = table.Column<float>(type: "real", nullable: false),
                    IgMResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG = table.Column<float>(type: "real", nullable: false),
                    IgGResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG1 = table.Column<float>(type: "real", nullable: false),
                    IgG1Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG2 = table.Column<float>(type: "real", nullable: false),
                    IgG2Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG3 = table.Column<float>(type: "real", nullable: false),
                    IgG3Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG4 = table.Column<float>(type: "real", nullable: false),
                    IgG4Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultsAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultsAdmin_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResultsPatient",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IgA = table.Column<float>(type: "real", nullable: false),
                    IgAResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgM = table.Column<float>(type: "real", nullable: false),
                    IgMResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG = table.Column<float>(type: "real", nullable: false),
                    IgGResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG1 = table.Column<float>(type: "real", nullable: false),
                    IgG1Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG2 = table.Column<float>(type: "real", nullable: false),
                    IgG2Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG3 = table.Column<float>(type: "real", nullable: false),
                    IgG3Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgG4 = table.Column<float>(type: "real", nullable: false),
                    IgG4Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultsPatient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultsPatient_Users_PatientId",
                        column: x => x.PatientId,
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
                name: "IX_TestResultsAdmin_PatientId",
                table: "TestResultsAdmin",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultsPatient_PatientId",
                table: "TestResultsPatient",
                column: "PatientId");

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
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TestResultsAdmin");

            migrationBuilder.DropTable(
                name: "TestResultsPatient");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
