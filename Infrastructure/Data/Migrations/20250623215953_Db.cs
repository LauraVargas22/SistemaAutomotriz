using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_UserMemberId",
                table: "refresh_tokens");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "refresh_tokens",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "expire",
                table: "refresh_tokens",
                newName: "expires");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "refresh_tokens",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UserMemberId",
                table: "refresh_tokens",
                newName: "user_member_id");

            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_UserMemberId",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_user_member_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "revoked",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expires",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_user_member_id",
                table: "refresh_tokens",
                column: "user_member_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_user_member_id",
                table: "refresh_tokens");

            migrationBuilder.RenameColumn(
                name: "user_member_id",
                table: "refresh_tokens",
                newName: "UserMemberId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "refresh_tokens",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "expires",
                table: "refresh_tokens",
                newName: "expire");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "refresh_tokens",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_user_member_id",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_UserMemberId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "revoked",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expire",
                table: "refresh_tokens",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_UserMemberId",
                table: "refresh_tokens",
                column: "UserMemberId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
