using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class fixbug2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalMessageContents_NotificationTypes_NotificationTypeType",
                table: "GlobalMessageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_GlobalMessages_GlobalMessageContents_MessageContentId",
                table: "GlobalMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_GlobalMessageContents_ContentId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalMessageContents",
                table: "GlobalMessageContents");

            migrationBuilder.RenameTable(
                name: "GlobalMessageContents",
                newName: "MessageContents");

            migrationBuilder.RenameIndex(
                name: "IX_GlobalMessageContents_NotificationTypeType",
                table: "MessageContents",
                newName: "IX_MessageContents_NotificationTypeType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageContents",
                table: "MessageContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalMessages_MessageContents_MessageContentId",
                table: "GlobalMessages",
                column: "MessageContentId",
                principalTable: "MessageContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_NotificationTypes_NotificationTypeType",
                table: "MessageContents",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageContents_ContentId",
                table: "Messages",
                column: "ContentId",
                principalTable: "MessageContents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalMessages_MessageContents_MessageContentId",
                table: "GlobalMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_NotificationTypes_NotificationTypeType",
                table: "MessageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageContents_ContentId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageContents",
                table: "MessageContents");

            migrationBuilder.RenameTable(
                name: "MessageContents",
                newName: "GlobalMessageContents");

            migrationBuilder.RenameIndex(
                name: "IX_MessageContents_NotificationTypeType",
                table: "GlobalMessageContents",
                newName: "IX_GlobalMessageContents_NotificationTypeType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalMessageContents",
                table: "GlobalMessageContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalMessageContents_NotificationTypes_NotificationTypeType",
                table: "GlobalMessageContents",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalMessages_GlobalMessageContents_MessageContentId",
                table: "GlobalMessages",
                column: "MessageContentId",
                principalTable: "GlobalMessageContents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_GlobalMessageContents_ContentId",
                table: "Messages",
                column: "ContentId",
                principalTable: "GlobalMessageContents",
                principalColumn: "Id");
        }
    }
}
