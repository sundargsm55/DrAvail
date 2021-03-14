using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailability9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliabilities_Doctors_DoctorID",
                table: "Avaliabilities");

            migrationBuilder.DropIndex(
                name: "IX_Avaliabilities_DoctorID",
                table: "Avaliabilities");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Avaliabilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Avaliabilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliabilities_DoctorID",
                table: "Avaliabilities",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliabilities_Doctors_DoctorID",
                table: "Avaliabilities",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
