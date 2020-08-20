using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class InitandProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.Sql(@"
CREATE PROCEDURE GetCompetitors
@FROM AS DateTime 
    AS 
SELECT m.Id,m.FName,m.LName,g.Gov,c.CityName,COUNT(DISTINCT v.DoctorId) AS UniqueVisits,
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
    ORDER BY UniqueEvaluators ASC,MAX(v.DateTime) ASC
");
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Roles = table.Column<string>(nullable: true),
                    Type = table.Column<bool>(nullable: false),
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
                    Ranking = table.Column<long>(nullable: false),
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
                name: "Governments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gov = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRepPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRepPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSpecializeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecializeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Type = table.Column<int>(nullable: false),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshingTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshingTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshingTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovernmentId = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Governments_GovernmentId",
                        column: x => x.GovernmentId,
                        principalTable: "Governments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    NotificationTypeType = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageContents_NotificationTypes_NotificationTypeType",
                        column: x => x.NotificationTypeType,
                        principalTable: "NotificationTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "MedicalReps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true),
                    ProfileUrl = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true),
                    WorkedOnCompany = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    VisitsCount = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    DisLikeCount = table.Column<int>(nullable: false),
                    UniqueUsers = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    DeActiveDate = table.Column<DateTime>(nullable: false),
                    PermanentDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    MedicalRepPositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalReps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalReps_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalReps_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalReps_MedicalRepPositions_MedicalRepPositionId",
                        column: x => x.MedicalRepPositionId,
                        principalTable: "MedicalRepPositions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlobalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    MessageContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessages_MessageContents_MessageContentId",
                        column: x => x.MessageContentId,
                        principalTable: "MessageContents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    MedicalSpecializedId = table.Column<int>(nullable: false),
                    AdderMedicalRepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_MedicalReps_AdderMedicalRepId",
                        column: x => x.AdderMedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctors_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctors_MedicalSpecializeds_MedicalSpecializedId",
                        column: x => x.MedicalSpecializedId,
                        principalTable: "MedicalSpecializeds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    DataId = table.Column<int>(nullable: false),
                    NotificationTypeType = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Read = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventNotifications_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventNotifications_NotificationTypes_NotificationTypeType",
                        column: x => x.NotificationTypeType,
                        principalTable: "NotificationTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    ContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MessageContents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "MessageContents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_MedicalReps_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "GlobalMessageReading",
                columns: table => new
                {
                    ReaderId = table.Column<int>(nullable: false),
                    GlobalMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessageReading", x => new { x.ReaderId, x.GlobalMessageId });
                    table.ForeignKey(
                        name: "FK_GlobalMessageReading_GlobalMessages_GlobalMessageId",
                        column: x => x.GlobalMessageId,
                        principalTable: "GlobalMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GlobalMessageReading_MedicalReps_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visits_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<int>(nullable: false),
                    EvaluatorId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Type = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_MedicalReps_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evaluations_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 0, "{UserName} makes a {like/dislike} to you comment in Dr/{doctorName}  : {visit} " });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 1, "{message Content}" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 2, "{message Content}" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernmentId",
                table: "Cities",
                column: "GovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AdderMedicalRepId",
                table: "Doctors",
                column: "AdderMedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CityId",
                table: "Doctors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalSpecializedId",
                table: "Doctors",
                column: "MedicalSpecializedId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatorId",
                table: "Evaluations",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_VisitId",
                table: "Evaluations",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_MedicalRepId",
                table: "EventNotifications",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_NotificationTypeType",
                table: "EventNotifications",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessageReading_GlobalMessageId",
                table: "GlobalMessageReading",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_MessageContentId",
                table: "GlobalMessages",
                column: "MessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReps_CityId",
                table: "MedicalReps",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReps_IdentityUserId",
                table: "MedicalReps",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReps_MedicalRepPositionId",
                table: "MedicalReps",
                column: "MedicalRepPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_NotificationTypeType",
                table: "MessageContents",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ContentId",
                table: "Messages",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshingTokens_UserId",
                table: "RefreshingTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitReport_ReporterId",
                table: "VisitReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitReport_VisitId",
                table: "VisitReport",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_MedicalRepId",
                table: "Visits",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_CompetitionId",
                table: "Winners",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_MedicalRepId",
                table: "Winners",
                column: "MedicalRepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CurrentWinners");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "EventNotifications");

            migrationBuilder.DropTable(
                name: "GlobalMessageReading");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RefreshingTokens");

            migrationBuilder.DropTable(
                name: "VisitReport");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GlobalMessages");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "MessageContents");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "MedicalReps");

            migrationBuilder.DropTable(
                name: "MedicalSpecializeds");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MedicalRepPositions");

            migrationBuilder.DropTable(
                name: "Governments");
        }
    }
}
