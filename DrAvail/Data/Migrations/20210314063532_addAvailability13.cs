using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class addAvailability13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.AlterColumn<decimal>(
                name: "Experience",
                table: "Doctors",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentAvaliabilityID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.AlterColumn<decimal>(
                name: "Experience",
                table: "Doctors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentAvaliabilityID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
