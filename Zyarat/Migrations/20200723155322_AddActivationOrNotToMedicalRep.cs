using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddActivationOrNotToMedicalRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "MedicalReps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "MedicalReps");
        }
    }
}
