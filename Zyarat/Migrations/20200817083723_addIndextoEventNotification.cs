using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class addIndextoEventNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventNotifications",
                table: "EventNotifications");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventNotifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventNotifications",
                table: "EventNotifications",
                columns: new[] { "DataId", "NotificationTypeId" });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} makes a {like/dislike} to your comment in Dr/{doctorName}  {visit} ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventNotifications",
                table: "EventNotifications");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EventNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventNotifications",
                table: "EventNotifications",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0,
                column: "Template",
                value: "{UserName} makes a {like/dislike} to you comment in Dr/{doctorName}  : {visit} ");
        }
    }
}
