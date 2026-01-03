using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Edit_tableEvent_Event_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isactive",
                schema: "MST",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isactive",
                schema: "MST",
                table: "Event");
        }
    }
}
