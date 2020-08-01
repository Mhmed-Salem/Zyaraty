using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class ModifyProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Roles = table.Column<string>(nullable: true),
                    MinUniqueUser = table.Column<int>(nullable: false),
                    MinUniqueVisit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentWinners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true),
                    Gov = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    UniqueVisits = table.Column<int>(nullable: false),
                    UniqueEvaluators = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Winners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    MedicalRepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Winners_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Winners_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Winners_CompetitionId",
                table: "Winners",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_MedicalRepId",
                table: "Winners",
                column: "MedicalRepId");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCurrentWinners
@FROM AS DateTime 
    AS 
SELECT top(20) m.Id,m.FName,m.LName,g.Gov,c.CityName,COUNT(DISTINCT v.DoctorId) AS UniqueVisits,
COUNT(DISTINCT e.EvaluatorId) AS UniqueEvaluators
FROM MedicalReps AS m INNER JOIN Visits AS v
    ON m.Id=v.MedicalRepId
INNER JOIN Evaluations AS e 
    ON v.Id=e.VisitId
INNER JOIN Cities AS c 
    ON c.Id=m.CityId
INNER JOIN Governments AS g
    ON g.Id=c.GovernmentId
WHERE m.Active=1 AND m.PermanentDeleted=0 AND e.Type=1 AND v.DateTime>=@FROM AND v.DateTime<GETDATE()
GROUP BY m.Id,m.FName,m.LName,g.Gov,c.CityName
    ORDER BY UniqueEvaluators ASC,MAX(v.DateTime) ASC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentWinners");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "Competitions");
        }
    }
}
