using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailabilityThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Avaliablities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliablities_DoctorID",
                table: "Avaliablities",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliablities_Doctors_DoctorID",
                table: "Avaliablities",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliablities_Doctors_DoctorID",
                table: "Avaliablities");

            migrationBuilder.DropIndex(
                name: "IX_Avaliablities_DoctorID",
                table: "Avaliablities");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Avaliablities");
        }
    }
}
