using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class modNotificatiobs1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalNotificationReadings");

            migrationBuilder.DropTable(
                name: "GlobalMessages");

            migrationBuilder.CreateTable(
                name: "Messages",
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
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MessageContents_MessageContentId",
                        column: x => x.MessageContentId,
                        principalTable: "MessageContents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    GlobalMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReadings_Messages_GlobalMessageId",
                        column: x => x.GlobalMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageReadings_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadings_GlobalMessageId",
                table: "MessageReadings",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadings_MedicalRepId",
                table: "MessageReadings",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageContentId",
                table: "Messages",
                column: "MessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_NotificationTypeId",
                table: "Messages",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReadings");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.CreateTable(
                name: "GlobalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageContentId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalMessageId = table.Column<int>(type: "int", nullable: false),
                    MedicalRepId = table.Column<int>(type: "int", nullable: false)
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
        }
    }
}
