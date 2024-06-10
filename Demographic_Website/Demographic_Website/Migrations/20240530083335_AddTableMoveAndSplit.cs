using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class AddTableMoveAndSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoveResidences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopulationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenIdentificationCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PopulationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveResidences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveResidences_Populations_PopulationId",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "PopulationId");
                });

            migrationBuilder.CreateTable(
                name: "SpiltResidentces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopulationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenIdentificationCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PopulationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiltResidentces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpiltResidentces_Populations_PopulationId",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "PopulationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveResidences_PopulationId",
                table: "MoveResidences",
                column: "PopulationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpiltResidentces_PopulationId",
                table: "SpiltResidentces",
                column: "PopulationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveResidences");

            migrationBuilder.DropTable(
                name: "SpiltResidentces");
        }
    }
}
