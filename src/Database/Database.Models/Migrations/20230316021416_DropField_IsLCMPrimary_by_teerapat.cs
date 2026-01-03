using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class DropField_IsLCMPrimary_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLCMPrimary",
                schema: "USR",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLCMPrimary",
                schema: "USR",
                table: "User",
                nullable: true);
        }
    }
}
