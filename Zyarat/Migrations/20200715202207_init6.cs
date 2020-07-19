using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReps_AspNetUsers_IdentityUserId1",
                table: "MedicalReps");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReps_IdentityUserId1",
                table: "MedicalReps");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "MedicalReps");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "MedicalReps",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReps_IdentityUserId",
                table: "MedicalReps",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReps_AspNetUsers_IdentityUserId",
                table: "MedicalReps",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReps_AspNetUsers_IdentityUserId",
                table: "MedicalReps");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReps_IdentityUserId",
                table: "MedicalReps");

            migrationBuilder.AlterColumn<int>(
                name: "IdentityUserId",
                table: "MedicalReps",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "MedicalReps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReps_IdentityUserId1",
                table: "MedicalReps",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReps_AspNetUsers_IdentityUserId1",
                table: "MedicalReps",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
