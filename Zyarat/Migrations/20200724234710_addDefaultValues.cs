using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class addDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitReport_Visits_VisitId",
                table: "VisitReport");

            migrationBuilder.AlterColumn<bool>(
                name: "PermanentDeleted",
                table: "MedicalReps",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "MedicalReps",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitReport_Visits_VisitId",
                table: "VisitReport",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitReport_Visits_VisitId",
                table: "VisitReport");

            migrationBuilder.AlterColumn<bool>(
                name: "PermanentDeleted",
                table: "MedicalReps",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "MedicalReps",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitReport_Visits_VisitId",
                table: "VisitReport",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }
    }
}
