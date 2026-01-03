using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Edit_table_Event_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactRegister_Project_PojectID",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropIndex(
                name: "IX_ContactRegister_PojectID",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropColumn(
                name: "PojectID",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropColumn(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropColumn(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.DropColumn(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactRegister");

            migrationBuilder.RenameColumn(
                name: "LastMigrateDate",
                schema: "MST",
                table: "Event",
                newName: "EventDateTo");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                schema: "MST",
                table: "Event",
                newName: "EventDateFrom");

            migrationBuilder.AddColumn<Guid>(
                name: "EventID",
                schema: "MST",
                table: "ProjectInEvent",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Queue",
                schema: "CTM",
                table: "ContactRegister",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInEvent_EventID",
                schema: "MST",
                table: "ProjectInEvent",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInEvent_Event_EventID",
                schema: "MST",
                table: "ProjectInEvent",
                column: "EventID",
                principalSchema: "MST",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInEvent_Event_EventID",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInEvent_EventID",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.DropColumn(
                name: "EventID",
                schema: "MST",
                table: "ProjectInEvent");

            migrationBuilder.RenameColumn(
                name: "EventDateTo",
                schema: "MST",
                table: "Event",
                newName: "LastMigrateDate");

            migrationBuilder.RenameColumn(
                name: "EventDateFrom",
                schema: "MST",
                table: "Event",
                newName: "EventDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "MST",
                table: "ProjectInEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "ProjectInEvent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "ProjectInEvent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "ProjectInEvent",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "MST",
                table: "Event",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "MST",
                table: "Event",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "MST",
                table: "Event",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Queue",
                schema: "CTM",
                table: "ContactRegister",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMigrateDate",
                schema: "CTM",
                table: "ContactRegister",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PojectID",
                schema: "CTM",
                table: "ContactRegister",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID1",
                schema: "CTM",
                table: "ContactRegister",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID2",
                schema: "CTM",
                table: "ContactRegister",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefMigrateID3",
                schema: "CTM",
                table: "ContactRegister",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_PojectID",
                schema: "CTM",
                table: "ContactRegister",
                column: "PojectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactRegister_Project_PojectID",
                schema: "CTM",
                table: "ContactRegister",
                column: "PojectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
