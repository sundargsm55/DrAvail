using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class ModifiedAvailabilityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CommonAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Avaliabilities");

            migrationBuilder.AddColumn<int>(
                name: "Pincode",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pincode",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CommonDays_MorningStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommonDays_MorningEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommonDays_EveningStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommonDays_EveningEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvailableOnWeekend = table.Column<bool>(type: "bit", nullable: false),
                    Weekends_MorningStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weekends_MorningEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weekends_EveningStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weekends_EveningEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactPreference = table.Column<int>(type: "int", nullable: false),
                    HospitalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Availabilities_Hospitals_HospitalID",
                        column: x => x.HospitalID,
                        principalTable: "Hospitals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_HospitalID",
                table: "Availabilities",
                column: "HospitalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Availabilities_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID",
                principalTable: "Availabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Availabilities_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID",
                principalTable: "Availabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Availabilities_CommonAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Availabilities_CurrentAvaliabilityID",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "Avaliabilities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactPreference = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HospitalID = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliabilities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Avaliabilities_Hospitals_HospitalID",
                        column: x => x.HospitalID,
                        principalTable: "Hospitals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliabilities_HospitalID",
                table: "Avaliabilities",
                column: "HospitalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CommonAvaliabilityID",
                table: "Doctors",
                column: "CommonAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaliabilities_CurrentAvaliabilityID",
                table: "Doctors",
                column: "CurrentAvaliabilityID",
                principalTable: "Avaliabilities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
