using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class ModifyVisitsReport2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<int>(nullable: false),
                    ReporterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitReport_MedicalReps_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitReport_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitReport_ReporterId",
                table: "VisitReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitReport_VisitId",
                table: "VisitReport",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitReport");
        }
    }
}
