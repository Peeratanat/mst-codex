using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.AuditLogs.Migrations
{
    public partial class ChangeCreateByType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferPromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingPromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItemLog",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreateBy",
                schema: "PRJ",
                table: "UnitLog",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferPromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterTransferCreditCardItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterPreSalePromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingPromotionItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItemLogs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRM",
                table: "MasterBookingCreditCardItemLog",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                schema: "PRJ",
                table: "UnitLog",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(Guid),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
