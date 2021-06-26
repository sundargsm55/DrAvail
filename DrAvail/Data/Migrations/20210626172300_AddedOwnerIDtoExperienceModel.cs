using Microsoft.EntityFrameworkCore.Migrations;

namespace DrAvail.Data.Migrations
{
    public partial class AddedOwnerIDtoExperienceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Experiences");
        }
    }
}
