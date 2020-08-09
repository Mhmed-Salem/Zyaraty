using Microsoft.EntityFrameworkCore.Migrations;

namespace Zyarat.Migrations
{
    public partial class finishpersitentNotificationService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalMessageReadings");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GlobalMessageReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalMessageId = table.Column<int>(type: "int", nullable: false),
                    MedicalRepId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_GlobalMessageReadings_GlobalMessageId",
                table: "GlobalMessageReadings",
                column: "GlobalMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalMessageReadings_MedicalRepId",
                table: "GlobalMessageReadings",
                column: "MedicalRepId");
        }
    }
}
