using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_type_vehicle_TypeVehicleId",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "TypeVehicleId",
                table: "vehicles",
                newName: "type_vehicle_id");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_TypeVehicleId",
                table: "vehicles",
                newName: "IX_vehicles_type_vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_type_vehicle_type_vehicle_id",
                table: "vehicles",
                column: "type_vehicle_id",
                principalTable: "type_vehicle",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_type_vehicle_type_vehicle_id",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "type_vehicle_id",
                table: "vehicles",
                newName: "TypeVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_type_vehicle_id",
                table: "vehicles",
                newName: "IX_vehicles_TypeVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_type_vehicle_TypeVehicleId",
                table: "vehicles",
                column: "TypeVehicleId",
                principalTable: "type_vehicle",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
