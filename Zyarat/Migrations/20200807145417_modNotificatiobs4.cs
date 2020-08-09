using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class modNotificatiobs4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_NotificationTypes_TypeId",
                table: "MessageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Messages_NotificationTypeId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_MessageContents_TypeId",
                table: "MessageContents");

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "MessageContents");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "NotificationTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeType",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeType",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type1",
                table: "MessageContents",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes",
                column: "Type");

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 0, "{UserName} make a {like/dislike} to you comment in Dr/{doctorName}" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 1, "{message Content}" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Type", "Template" },
                values: new object[] { 2, "{message Content}" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeType",
                table: "Notifications",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_NotificationTypeType",
                table: "Messages",
                column: "NotificationTypeType");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_Type1",
                table: "MessageContents",
                column: "Type1");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_NotificationTypes_Type1",
                table: "MessageContents",
                column: "Type1",
                principalTable: "NotificationTypes",
                principalColumn: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeType",
                table: "Messages",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeType",
                table: "Notifications",
                column: "NotificationTypeType",
                principalTable: "NotificationTypes",
                principalColumn: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageContents_NotificationTypes_Type1",
                table: "MessageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeType",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeType",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationTypeType",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Messages_NotificationTypeType",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_MessageContents_Type1",
                table: "MessageContents");

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Type",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "NotificationTypeType",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationTypeType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Type1",
                table: "MessageContents");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "NotificationTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "NotificationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "MessageContents",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTypes",
                table: "NotificationTypes",
                column: "Id");

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
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_NotificationTypeId",
                table: "Messages",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageContents_TypeId",
                table: "MessageContents",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageContents_NotificationTypes_TypeId",
                table: "MessageContents",
                column: "TypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_NotificationTypes_NotificationTypeId",
                table: "Messages",
                column: "NotificationTypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id");
        }
    }
}
