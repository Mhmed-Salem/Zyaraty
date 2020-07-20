using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddvisitType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "Visits");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Visits",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Visits");

            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "Visits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
