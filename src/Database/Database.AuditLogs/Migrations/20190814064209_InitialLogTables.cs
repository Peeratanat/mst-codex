using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.AuditLogs.Migrations
{
    public partial class InitialLogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PRJ");

            migrationBuilder.EnsureSchema(
                name: "PRM");

            migrationBuilder.CreateTable(
                name: "UnitLog",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingCreditCardItemLog",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingCreditCardItemLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingPromotionFreeItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingPromotionFreeItemLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingPromotionItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingPromotionItemLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSalePromotionItemLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferCreditCardItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferCreditCardItemLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferPromotionFreeItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferPromotionFreeItemLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferPromotionItemLogs",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    KeyValue = table.Column<Guid>(nullable: false),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferPromotionItemLogs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitLog",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "MasterBookingCreditCardItemLog",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingPromotionFreeItemLogs",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingPromotionItemLogs",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionItemLogs",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferCreditCardItemLogs",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferPromotionFreeItemLogs",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferPromotionItemLogs",
                schema: "PRM");
        }
    }
}
