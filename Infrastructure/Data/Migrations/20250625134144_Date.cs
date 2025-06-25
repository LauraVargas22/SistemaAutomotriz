using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_user_member_id",
                table: "refresh_tokens");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "roles",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "roles",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "refresh_tokens",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "refresh_tokens",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "user_member_id",
                table: "refresh_tokens",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_user_member_id",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_user_id");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "vehicles",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "vehicles",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "users",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "users",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "type_vehicle",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "type_vehicle",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "type_service",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "type_service",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "state",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "state",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "specializations",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "specializations",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "spare_part",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "spare_part",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "service_order",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "exit_date",
                table: "service_order",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "entry_date",
                table: "service_order",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "service_order",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "roles",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "roles",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "revoked",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "expires",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "refresh_tokens",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "refresh_tokens",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "order_details",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "order_details",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "invoice",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "invoice",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "invoice",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "inspection",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "inspection",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "diagnostics",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "diagnostics",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "diagnostics",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "detaill_inspection",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "detaill_inspection",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "clients",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "clients",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "birth",
                table: "clients",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "updatedAt",
                table: "auditories",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "createdAt",
                table: "auditories",
                type: "date",
                nullable: false,
                defaultValueSql: "CURRENT_DATE",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_user_id",
                table: "refresh_tokens");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "roles",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "roles",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "refresh_tokens",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "refresh_tokens",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "refresh_tokens",
                newName: "user_member_id");

            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_user_member_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "vehicles",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "vehicles",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "users",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "users",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "type_vehicle",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "type_vehicle",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "type_service",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "type_service",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "state",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "state",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "specializations",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "specializations",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "spare_part",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "spare_part",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "service_order",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "exit_date",
                table: "service_order",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "entry_date",
                table: "service_order",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "service_order",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "roles",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "roles",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "revoked",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "expires",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "order_details",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "order_details",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "invoice",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "invoice",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "invoice",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "inspection",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "inspection",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "diagnostics",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "diagnostics",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "diagnostics",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "detaill_inspection",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "detaill_inspection",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "clients",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "clients",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth",
                table: "clients",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedAt",
                table: "auditories",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "auditories",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "CURRENT_DATE");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_user_member_id",
                table: "refresh_tokens",
                column: "user_member_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
