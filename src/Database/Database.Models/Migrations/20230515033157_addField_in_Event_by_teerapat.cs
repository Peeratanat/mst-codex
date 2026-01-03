using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class addField_in_Event_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                schema: "CTM",
                table: "ContactRegister",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnitControlInterest",
                schema: "PRJ",
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
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    InterestCounter = table.Column<int>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitControlInterest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitControlInterest_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlInterest_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlInterest_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlInterest_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitControlLock",
                schema: "PRJ",
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
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    FloorID = table.Column<Guid>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitControlLock", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitControlLock_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlLock_Floor_FloorID",
                        column: x => x.FloorID,
                        principalSchema: "PRJ",
                        principalTable: "Floor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlLock_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlLock_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitControlLock_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingControl",
                schema: "SAL",
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
                    BookingID = table.Column<Guid>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    BookingLockMasterCenterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingControl", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingControl_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingControl_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingControl_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingControl_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingControl_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlInterest_CreatedByUserID",
                schema: "PRJ",
                table: "UnitControlInterest",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlInterest_ProjectID",
                schema: "PRJ",
                table: "UnitControlInterest",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlInterest_UnitID",
                schema: "PRJ",
                table: "UnitControlInterest",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlInterest_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitControlInterest",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlLock_CreatedByUserID",
                schema: "PRJ",
                table: "UnitControlLock",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlLock_FloorID",
                schema: "PRJ",
                table: "UnitControlLock",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlLock_ProjectID",
                schema: "PRJ",
                table: "UnitControlLock",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlLock_UnitID",
                schema: "PRJ",
                table: "UnitControlLock",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitControlLock_UpdatedByUserID",
                schema: "PRJ",
                table: "UnitControlLock",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingControl_BookingID",
                schema: "SAL",
                table: "BookingControl",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingControl_CreatedByUserID",
                schema: "SAL",
                table: "BookingControl",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingControl_ProjectID",
                schema: "SAL",
                table: "BookingControl",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingControl_UnitID",
                schema: "SAL",
                table: "BookingControl",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingControl_UpdatedByUserID",
                schema: "SAL",
                table: "BookingControl",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitControlInterest",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "UnitControlLock",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BookingControl",
                schema: "SAL");

            migrationBuilder.DropColumn(
                name: "Prefix",
                schema: "CTM",
                table: "ContactRegister");
        }
    }
}
