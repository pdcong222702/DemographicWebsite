using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentCodeStaying",
                table: "TemporarilyStaying",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TemporarilyStaying",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentCodeStaying",
                table: "TemporarilyStaying");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TemporarilyStaying");
        }
    }
}
