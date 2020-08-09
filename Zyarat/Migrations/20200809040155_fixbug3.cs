using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class fixbug3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalMessages_NotificationTypes_NotificationTypeType",
                table: "GlobalMessages");

            migrationBuilder.DropIndex(
                name: "IX_GlobalMessages_NotificationTypeType",
                table: "GlobalMessages");

            migrationBuilder.DropColumn(
                name: "NotificationTypeType",
                table: "GlobalMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeType",
                table: "GlobalMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_NotificationTypeType",
                table: "GlobalMessages",
                column: "NotificationTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalMessages_NotificationTypes_NotificationTypeType",
                table: "GlobalMessages",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");
        }
    }
}
