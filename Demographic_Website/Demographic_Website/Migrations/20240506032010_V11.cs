using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "alive",
                table: "Populations",
                newName: "Alive");

            migrationBuilder.RenameColumn(
                name: "DateOfDate",
                table: "Populations",
                newName: "DateOfDeath");

            migrationBuilder.AddColumn<string>(
                name: "EducationalLevels",
                table: "Populations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationalLevels",
                table: "Populations");

            migrationBuilder.RenameColumn(
                name: "Alive",
                table: "Populations",
                newName: "alive");

            migrationBuilder.RenameColumn(
                name: "DateOfDeath",
                table: "Populations",
                newName: "DateOfDate");
        }
    }
}
