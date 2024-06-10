using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResidenceBooklet",
                columns: table => new
                {
                    ResidenceBookletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidenceBookletCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BookletArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Residenc__AE46D5A2295AD5E1", x => x.ResidenceBookletId);
                });

            migrationBuilder.CreateTable(
                name: "Populations",
                columns: table => new
                {
                    PopulationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopulationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PopulationNickName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CitizenIdentificationCard = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    DateOfIssue = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfIssue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ResidenceBookletId = table.Column<int>(type: "int", nullable: true),
                    Relationship = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Populati__3A2E15E20414EC74", x => x.PopulationId);
                    table.ForeignKey(
                        name: "FK_ResidenceBooklet",
                        column: x => x.ResidenceBookletId,
                        principalTable: "ResidenceBooklet",
                        principalColumn: "ResidenceBookletId");
                });

            migrationBuilder.CreateTable(
                name: "TemporarilyAbsent",
                columns: table => new
                {
                    TemporarilyAbsentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopulationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CitizenIdentificationCard = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    PassportCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Temporar__FB890209EFD0C9ED", x => x.TemporarilyAbsentId);
                    table.ForeignKey(
                        name: "FK_PopulationAbsent",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "PopulationId");
                });

            migrationBuilder.CreateTable(
                name: "TemporarilyStaying",
                columns: table => new
                {
                    TemporarilyAbsentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopulationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CitizenIdentificationCard = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    PassportCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TemporaryAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Temporar__FB89020962FB23B0", x => x.TemporarilyAbsentId);
                    table.ForeignKey(
                        name: "FK_PopulationStaying",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "PopulationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Populations_ResidenceBookletId",
                table: "Populations",
                column: "ResidenceBookletId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporarilyAbsent_PopulationId",
                table: "TemporarilyAbsent",
                column: "PopulationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporarilyStaying_PopulationId",
                table: "TemporarilyStaying",
                column: "PopulationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporarilyAbsent");

            migrationBuilder.DropTable(
                name: "TemporarilyStaying");

            migrationBuilder.DropTable(
                name: "Populations");

            migrationBuilder.DropTable(
                name: "ResidenceBooklet");
        }
    }
}
