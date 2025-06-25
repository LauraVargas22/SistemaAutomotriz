using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeVehicleId",
                table: "vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "type_vehicle",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_vehicle", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_TypeVehicleId",
                table: "vehicles",
                column: "TypeVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_type_vehicle_TypeVehicleId",
                table: "vehicles",
                column: "TypeVehicleId",
                principalTable: "type_vehicle",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_type_vehicle_TypeVehicleId",
                table: "vehicles");

            migrationBuilder.DropTable(
                name: "type_vehicle");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_TypeVehicleId",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "TypeVehicleId",
                table: "vehicles");
        }
    }
}
