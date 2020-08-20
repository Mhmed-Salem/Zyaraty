using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class AddNotificationSytenIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_EventNotifications_MedicalRepId",
                table: "EventNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId_DateTime",
                table: "Messages",
                columns: new[] { "ReceiverId", "DateTime" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessages_DateTime",
                table: "GlobalMessages",
                column: "DateTime")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_MedicalRepId_DateTime",
                table: "EventNotifications",
                columns: new[] { "MedicalRepId", "DateTime" })
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId_DateTime",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_GlobalMessages_DateTime",
                table: "GlobalMessages");

            migrationBuilder.DropIndex(
                name: "IX_EventNotifications_MedicalRepId_DateTime",
                table: "EventNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_EventNotifications_MedicalRepId",
                table: "EventNotifications",
                column: "MedicalRepId");
        }
    }
}
