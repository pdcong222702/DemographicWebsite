using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportCode",
                table: "TemporarilyStaying");

            migrationBuilder.DropColumn(
                name: "PassportCode",
                table: "TemporarilyAbsent");

            migrationBuilder.RenameColumn(
                name: "TemporarilyAbsentId",
                table: "TemporarilyStaying",
                newName: "TemporarilyStayingId");

            migrationBuilder.AddColumn<bool>(
                name: "IsNewStay",
                table: "TemporarilyStaying",
                type: "bit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNewAbsent",
                table: "TemporarilyAbsent",
                type: "bit",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNewStay",
                table: "TemporarilyStaying");

            migrationBuilder.DropColumn(
                name: "IsNewAbsent",
                table: "TemporarilyAbsent");

            migrationBuilder.RenameColumn(
                name: "TemporarilyStayingId",
                table: "TemporarilyStaying",
                newName: "TemporarilyAbsentId");

            migrationBuilder.AddColumn<string>(
                name: "PassportCode",
                table: "TemporarilyStaying",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportCode",
                table: "TemporarilyAbsent",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
