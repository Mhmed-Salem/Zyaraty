using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class SolveAbigInallProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create  PROCEDURE [dbo].[OnEndingMonthlyCompetition]
AS 

DECLARE @UUSERS INT ,
        @UVisits INT ,
        @CId int;

SELECT TOP(1)  
@UUSERS=c.MinUniqueUsers,
@UVisits=c.MinUniqueVisits,
@CId=c.Id
FROM Competitions AS c
WHERE c.Type=1 AND c.DateTime<getdate()
ORDER BY Id DESC ;

IF OBJECT_ID('tempdb.dbo.#Competitors') IS NOT NULL
DROP TABLE #Competitors;

CREATE TABLE #Competitors(
Ranking int ,
id int ,
CompetitionId int 
);


INSERT INTO #Competitors (Ranking,id)
SELECT TOP(20) ROW_NUMBER() OVER (ORDER BY COUNT(DISTINCT e.EvaluatorId) DESC,MAX(e.DateTime) DESC) AS Ranking
,m.Id
FROM MedicalReps AS m INNER JOIN Visits AS v
    ON m.Id=v.MedicalRepId
INNER JOIN Evaluations AS e 
    ON v.Id=e.VisitId
INNER JOIN Cities AS c 
    ON c.Id=m.CityId
INNER JOIN Governments AS g
    ON g.Id=c.GovernmentId
WHERE m.Active=1 AND m.PermanentDeleted=0 AND e.Type=1 AND v.DateTime>=DATEADD(MONTH,-1,GETDATE()) AND v.DateTime<GETDATE()
GROUP BY m.Id,m.FName,m.LName,g.Gov,c.CityName
HAVING COUNT(DISTINCT v.DoctorId)>=@UVisits AND COUNT(DISTINCT e.EvaluatorId)>=@UUSERS


UPDATE #Competitors set CompetitionId=@CId;

INSERT INTO Winners (Rank,MedicalRepId,CompetitionId)
SELECT X.Ranking,X.id,X.CompetitionId FROM #Competitors AS X;
");
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[OnEndingDailyCompetition]
AS 

DECLARE @UUSERS INT ,
        @UVisits INT ,
        @CId int;

SELECT TOP(1)  
@UUSERS=c.MinUniqueUsers,
@UVisits=c.MinUniqueVisits,
@CId=c.Id
FROM Competitions AS c
WHERE c.Type=0 AND c.DateTime<getdate()
ORDER BY Id DESC ;

IF OBJECT_ID('tempdb.dbo.#Competitors') IS NOT NULL
DROP TABLE #Competitors;

CREATE TABLE #Competitors(
Ranking int ,
id int ,
CompetitionId int 
);


INSERT INTO #Competitors (Ranking,id)
SELECT TOP(20) ROW_NUMBER() OVER (ORDER BY COUNT(DISTINCT e.EvaluatorId) DESC,MAX(e.DateTime) DESC) AS Ranking
,m.Id
FROM MedicalReps AS m INNER JOIN Visits AS v
    ON m.Id=v.MedicalRepId
INNER JOIN Evaluations AS e 
    ON v.Id=e.VisitId
INNER JOIN Cities AS c 
    ON c.Id=m.CityId
INNER JOIN Governments AS g
    ON g.Id=c.GovernmentId
WHERE m.Active=1 AND m.PermanentDeleted=0 AND e.Type=1 AND v.DateTime>=DATEADD(DAY,-1,GETDATE()) AND v.DateTime<GETDATE()
GROUP BY m.Id,m.FName,m.LName,g.Gov,c.CityName
HAVING COUNT(DISTINCT v.DoctorId)>=@UVisits AND COUNT(DISTINCT e.EvaluatorId)>=@UUSERS


UPDATE #Competitors set CompetitionId=@CId;

INSERT INTO Winners (Rank,MedicalRepId,CompetitionId)
SELECT X.Ranking,X.id,X.CompetitionId FROM #Competitors AS X;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
