using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class modNotificatiobs11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_NotificationTypes_Type1",
                table: "MessageContents");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_MessageContents_Type1",
                table: "MessageContents");

            migrationBuilder.DropColumn(
                name: "Type1",
                table: "MessageContents");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "MessageContents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeType",
                table: "MessageContents",
                nullable: false,
                defaultValue: 0);

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
                    Message = table.Column<string>(nullable: true)
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

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} makes a {like/dislike} to you comment in Dr/{doctorName}  : {visit} ");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_NotificationTypeType",
                table: "MessageContents",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_MedicalRepId",
                table: "EventNotifications",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_NotificationTypeType",
                table: "EventNotifications",
                column: "NotificationTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_NotificationTypes_NotificationTypeType",
                table: "MessageContents",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_NotificationTypes_NotificationTypeType",
                table: "MessageContents");

            migrationBuilder.DropTable(
                name: "EventNotifications");

            migrationBuilder.DropIndex(
                name: "IX_MessageContents_NotificationTypeType",
                table: "MessageContents");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "MessageContents");

            migrationBuilder.DropColumn(
                name: "NotificationTypeType",
                table: "MessageContents");

            migrationBuilder.AddColumn<int>(
                name: "Type1",
                table: "MessageContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalRepId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeType = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Notifications_NotificationTypes_NotificationTypeType",
                        column: x => x.NotificationTypeType,
                        principalTable: "NotificationTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} make a {like/dislike} to you comment in Dr/{doctorName}");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_Type1",
                table: "MessageContents",
                column: "Type1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MedicalRepId",
                table: "Notifications",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeType",
                table: "Notifications",
                column: "NotificationTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_NotificationTypes_Type1",
                table: "MessageContents",
                column: "Type1",
                principalTable: "NotificationTypes",
                principalColumn: "Type");
        }
    }
}
