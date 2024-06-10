using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookLet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidenceBookletId = table.Column<int>(type: "int", nullable: false),
                    PopulationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLet_Populations_PopulationId",
                        column: x => x.PopulationId,
                        principalTable: "Populations",
                        principalColumn: "PopulationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLet_ResidenceBooklet_ResidenceBookletId",
                        column: x => x.ResidenceBookletId,
                        principalTable: "ResidenceBooklet",
                        principalColumn: "ResidenceBookletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLet_PopulationId",
                table: "BookLet",
                column: "PopulationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLet_ResidenceBookletId",
                table: "BookLet",
                column: "ResidenceBookletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLet");
        }
    }
}
