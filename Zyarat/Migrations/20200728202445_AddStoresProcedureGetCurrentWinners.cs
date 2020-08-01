using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddStoresProcedureGetCurrentWinners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCurrentWinners
            @FROM AS DateTime 
                AS 
            SELECT top(20) m.Id,m.FName,m.LName,g.Gov,c.CityName,COUNT(DISTINCT v.DoctorId) AS UniqueVisits,COUNT(DISTINCT e.EvaluatorId) AS UniqueEvaluators
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
                ORDER BY UniqueVisits ASC,UniqueEvaluators ASC
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
