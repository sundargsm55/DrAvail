using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailability10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CommonAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_CommonAvaliabilityID",
                table: "Doctors");
        }
    }
}
