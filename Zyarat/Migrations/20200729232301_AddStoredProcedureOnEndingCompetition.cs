using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddStoredProcedureOnEndingCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ranking",
                table: "Winners",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.Sql(@"
CREATE PROCEDURE OnEndingCompetition
@from AS  DATETIME
AS 

DECLARE @UUSERS INT ,
        @UVisits INT ,
        @CId int;

SELECT TOP(1)  
@UUSERS=c.MinUniqueUser,
@UVisits=c.MinUniqueVisit,
@CId=c.Id
FROM Competitions AS c ORDER BY Id DESC ;

IF OBJECT_ID('tempdb.dbo.#Competitors') IS NOT NULL
DROP TABLE #Competitors;

CREATE TABLE #Competitors(
Ranking int ,
id int ,
CompetitionId int 
);


INSERT INTO #Competitors (Ranking,id)
SELECT TOP(20) ROW_NUMBER() OVER (ORDER BY COUNT(DISTINCT e.EvaluatorId) DESC,MAX(v.DateTime) DESC) AS Ranking
,m.Id
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
HAVING COUNT(DISTINCT v.DoctorId)>=@UVisits AND COUNT(DISTINCT e.EvaluatorId)>=@UUSERS;

update #Competitors set CompetitionId=@CId;

INSERT INTO Winners (Rank,MedicalRepId,CompetitionId)
SELECT X.Ranking,X.CompetitionId,X.CompetitionId FROM #Competitors AS X;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ranking",
                table: "Winners");
        }
    }
}
