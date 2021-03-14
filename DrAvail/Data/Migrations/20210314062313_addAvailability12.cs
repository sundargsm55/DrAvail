using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailability12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "CurrentAvaliabilityID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID",
                unique: true);
        }
    }
}
