using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class ModifyVisitsReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitBlocking");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeActiveDate",
                table: "MedicalReps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "PermanentDeleted",
                table: "MedicalReps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeActiveDate",
                table: "MedicalReps");

            migrationBuilder.DropColumn(
                name: "PermanentDeleted",
                table: "MedicalReps");

            migrationBuilder.CreateTable(
                name: "VisitBlocking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalRepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitBlocking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitBlocking_MedicalReps_MedicalRepId",
                        column: x => x.MedicalRepId,
                        principalTable: "MedicalReps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitBlocking_MedicalRepId",
                table: "VisitBlocking",
                column: "MedicalRepId",
                unique: true);
        }
    }
}
