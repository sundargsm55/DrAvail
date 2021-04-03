using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class AddOwnerIdforHospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Hospitals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Hospitals");
        }
    }
}
