using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddImagetoRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "MedicalReps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "MedicalReps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileUrl",
                table: "MedicalReps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "MedicalReps");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "MedicalReps");

            migrationBuilder.DropColumn(
                name: "ProfileUrl",
                table: "MedicalReps");
        }
    }
}
