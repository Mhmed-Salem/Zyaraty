using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class ModifyNamesInCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinUniqueUser",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "MinUniqueVisit",
                table: "Competitions");

            migrationBuilder.AddColumn<int>(
                name: "MinUniqueUsers",
                table: "Competitions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinUniqueVisits",
                table: "Competitions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinUniqueUsers",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "MinUniqueVisits",
                table: "Competitions");

            migrationBuilder.AddColumn<int>(
                name: "MinUniqueUser",
                table: "Competitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinUniqueVisit",
                table: "Competitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
