using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class LetterGuaranteeHistory_by_teerapat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LetterGuarantee_User_CancelByUserID",
                schema: "PRJ",
                table: "LetterGuarantee");

            migrationBuilder.DropIndex(
                name: "IX_LetterGuarantee_CancelByUserID",
                schema: "PRJ",
                table: "LetterGuarantee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                schema: "PRJ",
                table: "LetterGuarantee",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LetterGuaranteeFile",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LetterGuaranteeID = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterGuaranteeFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LetterGuaranteeFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LetterGuaranteeFile_LetterGuarantee_LetterGuaranteeID",
                        column: x => x.LetterGuaranteeID,
                        principalSchema: "PRJ",
                        principalTable: "LetterGuarantee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LetterGuaranteeFile_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LetterGuaranteeFile_CreatedByUserID",
                schema: "PRJ",
                table: "LetterGuaranteeFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LetterGuaranteeFile_LetterGuaranteeID",
                schema: "PRJ",
                table: "LetterGuaranteeFile",
                column: "LetterGuaranteeID");

            migrationBuilder.CreateIndex(
                name: "IX_LetterGuaranteeFile_UpdatedByUserID",
                schema: "PRJ",
                table: "LetterGuaranteeFile",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterGuaranteeFile",
                schema: "PRJ");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiredDate",
                schema: "PRJ",
                table: "LetterGuarantee",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LetterGuarantee_CancelByUserID",
                schema: "PRJ",
                table: "LetterGuarantee",
                column: "CancelByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_LetterGuarantee_User_CancelByUserID",
                schema: "PRJ",
                table: "LetterGuarantee",
                column: "CancelByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
