using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class NewDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageContents_MessageContentId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeType",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageContents");

            migrationBuilder.DropTable(
                name: "MessageReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageContentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_NotificationTypeType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageContentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NotificationTypeType",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Message",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GlobalMessageContents",
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
                    table.PrimaryKey("PK_GlobalMessageContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessageContents_NotificationTypes_NotificationTypeType",
                        column: x => x.NotificationTypeType,
                        principalTable: "NotificationTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "GlobalMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    MessageContentId = table.Column<int>(nullable: false),
                    NotificationTypeType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessages_GlobalMessageContents_MessageContentId",
                        column: x => x.MessageContentId,
                        principalTable: "GlobalMessageContents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GlobalMessages_NotificationTypes_NotificationTypeType",
                        column: x => x.NotificationTypeType,
                        principalTable: "NotificationTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "GlobalMessageReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRepId = table.Column<int>(nullable: false),
                    GlobalMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMessageReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalMessageReadings_GlobalMessages_GlobalMessageId",
                        column: x => x.GlobalMessageId,
                        principalTable: "GlobalMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GlobalMessageReadings_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessageContents_NotificationTypeType",
                table: "GlobalMessageContents",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessageReadings_GlobalMessageId",
                table: "GlobalMessageReadings",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessageReadings_MedicalRepId",
                table: "GlobalMessageReadings",
                column: "MedicalRepId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_MessageContentId",
                table: "GlobalMessages",
                column: "MessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_NotificationTypeType",
                table: "GlobalMessages",
                column: "NotificationTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MedicalReps_ReceiverId",
                table: "Message",
                column: "ReceiverId",
                principalTable: "MedicalReps",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_MedicalReps_ReceiverId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "GlobalMessageReadings");

            migrationBuilder.DropTable(
                name: "GlobalMessages");

            migrationBuilder.DropTable(
                name: "GlobalMessageContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ReceiverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageContentId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeType",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MessageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    NotificationTypeType = table.Column<int>(type: "int", nullable: false)
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
                name: "MessageReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalMessageId = table.Column<int>(type: "int", nullable: false),
                    MedicalRepId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Messages_MessageContentId",
                table: "Messages",
                column: "MessageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_NotificationTypeType",
                table: "Messages",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_NotificationTypeType",
                table: "MessageContents",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadings_GlobalMessageId",
                table: "MessageReadings",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadings_MedicalRepId",
                table: "MessageReadings",
                column: "MedicalRepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageContents_MessageContentId",
                table: "Messages",
                column: "MessageContentId",
                principalTable: "MessageContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeType",
                table: "Messages",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");
        }
    }
}
