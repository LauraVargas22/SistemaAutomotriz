using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Auditory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditories_users_user_id",
                table: "auditories");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "auditories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "type_action",
                table: "auditories",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "entity",
                table: "auditories",
                newName: "entity_name");

            migrationBuilder.RenameIndex(
                name: "IX_auditories_user_id",
                table: "auditories",
                newName: "IX_auditories_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "auditories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "change_type",
                table: "auditories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_auditories_users_UserId",
                table: "auditories",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditories_users_UserId",
                table: "auditories");

            migrationBuilder.DropColumn(
                name: "change_type",
                table: "auditories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "auditories",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "user",
                table: "auditories",
                newName: "type_action");

            migrationBuilder.RenameColumn(
                name: "entity_name",
                table: "auditories",
                newName: "entity");

            migrationBuilder.RenameIndex(
                name: "IX_auditories_UserId",
                table: "auditories",
                newName: "IX_auditories_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "auditories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_auditories_users_user_id",
                table: "auditories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
