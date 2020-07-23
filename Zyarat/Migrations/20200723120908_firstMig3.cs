using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class firstMig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Visits_VisitId",
                table: "Evaluations");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Visits_VisitId",
                table: "Evaluations",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Visits_VisitId",
                table: "Evaluations");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Visits_VisitId",
                table: "Evaluations",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }
    }
}
