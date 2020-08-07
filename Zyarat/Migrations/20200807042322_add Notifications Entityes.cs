using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class addNotificationsEntityes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Ranking",
                table: "CurrentWinners",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageContents_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    DataId = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlobalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    MessageContentId = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessages_MessageContents_MessageContentId",
                        column: x => x.MessageContentId,
                        principalTable: "MessageContents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GlobalMessages_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlobalNotificationReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    GlobalMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalNotificationReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalNotificationReadings_GlobalMessages_GlobalMessageId",
                        column: x => x.GlobalMessageId,
                        principalTable: "GlobalMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GlobalNotificationReadings_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Template", "Type" },
                values: new object[] { 1, "{UserName} make a {like/dislike} to you comment in Dr/{doctorName}", "evaluation" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Template", "Type" },
                values: new object[] { 2, "{message Content}", "message" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Template", "Type" },
                values: new object[] { 3, "{message Content}", "globalMessage" });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_MessageContentId",
                table: "GlobalMessages",
                column: "MessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_NotificationTypeId",
                table: "GlobalMessages",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalNotificationReadings_GlobalMessageId",
                table: "GlobalNotificationReadings",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalNotificationReadings_MedicalRepId",
                table: "GlobalNotificationReadings",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_TypeId",
                table: "MessageContents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MedicalRepId",
                table: "Notifications",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalNotificationReadings");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "GlobalMessages");

            migrationBuilder.DropTable(
                name: "MessageContents");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "Ranking",
                table: "CurrentWinners");
        }
    }
}
