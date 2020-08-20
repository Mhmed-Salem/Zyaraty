using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class improveNotificationMessageFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} had made a {like/dislike} to your comment in Dr/{doctorName}  \"{visit}\" ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} makes a {like/dislike} to your comment in Dr/{doctorName}  {visit} ");
        }
    }
}
