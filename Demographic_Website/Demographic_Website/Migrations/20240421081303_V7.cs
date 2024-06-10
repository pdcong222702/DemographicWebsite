using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demographic_Website.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MoveDate",
                table: "ResidenceBooklet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoveReason",
                table: "ResidenceBooklet",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveDate",
                table: "ResidenceBooklet");

            migrationBuilder.DropColumn(
                name: "MoveReason",
                table: "ResidenceBooklet");
        }
    }
}
