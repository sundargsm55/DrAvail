using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailability6 : Migration
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

            migrationBuilder.RenameColumn(
                name: "CommonAvailablityID",
                table: "Doctors",
                newName: "AvaliabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AvaliabilityID",
                table: "Doctors",
                column: "AvaliabilityID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_AvaliabilityID",
                table: "Doctors",
                column: "AvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_AvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AvaliabilityID",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "AvaliabilityID",
                table: "Doctors",
                newName: "CommonAvailablityID");

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
