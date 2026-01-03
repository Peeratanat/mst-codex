using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class altertable_Event_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    NameTH = table.Column<string>(maxLength: 100, nullable: true),
                    NameEN = table.Column<string>(maxLength: 100, nullable: true),
                    EventDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Event_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInEvent",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    PojectID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInEvent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectInEvent_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInEvent_Project_PojectID",
                        column: x => x.PojectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInEvent_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactRegister",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    ContactID = table.Column<Guid>(nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: true),
                    EventID = table.Column<Guid>(nullable: true),
                    PojectID = table.Column<Guid>(nullable: true),
                    Queue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRegister", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactRegister_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactRegister_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactRegister_Event_EventID",
                        column: x => x.EventID,
                        principalSchema: "MST",
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactRegister_Project_PojectID",
                        column: x => x.PojectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactRegister_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_ContactID",
                schema: "CTM",
                table: "ContactRegister",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_CreatedByUserID",
                schema: "CTM",
                table: "ContactRegister",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_EventID",
                schema: "CTM",
                table: "ContactRegister",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_PojectID",
                schema: "CTM",
                table: "ContactRegister",
                column: "PojectID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRegister_UpdatedByUserID",
                schema: "CTM",
                table: "ContactRegister",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CreatedByUserID",
                schema: "MST",
                table: "Event",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UpdatedByUserID",
                schema: "MST",
                table: "Event",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInEvent_CreatedByUserID",
                schema: "MST",
                table: "ProjectInEvent",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInEvent_PojectID",
                schema: "MST",
                table: "ProjectInEvent",
                column: "PojectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInEvent_UpdatedByUserID",
                schema: "MST",
                table: "ProjectInEvent",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRegister",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "ProjectInEvent",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "MST");
        }
    }
}
