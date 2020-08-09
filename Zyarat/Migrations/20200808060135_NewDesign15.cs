using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class NewDesign15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ContentId",
                table: "Messages",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_GlobalMessageContents_ContentId",
                table: "Messages",
                column: "ContentId",
                principalTable: "GlobalMessageContents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_GlobalMessageContents_ContentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ContentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
