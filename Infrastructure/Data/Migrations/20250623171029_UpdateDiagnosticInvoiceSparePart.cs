using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiagnosticInvoiceSparePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_service_order_ServiceOrderId",
                table: "invoice");

            migrationBuilder.RenameColumn(
                name: "ServiceOrderId",
                table: "invoice",
                newName: "service_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_ServiceOrderId",
                table: "invoice",
                newName: "IX_invoice_service_order_id");

            migrationBuilder.AddColumn<int>(
                name: "max_stock",
                table: "spare_part",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "invoice",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "diagnostics",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_service_order_service_order_id",
                table: "invoice",
                column: "service_order_id",
                principalTable: "service_order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_service_order_service_order_id",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "max_stock",
                table: "spare_part");

            migrationBuilder.DropColumn(
                name: "code",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "date",
                table: "diagnostics");

            migrationBuilder.RenameColumn(
                name: "service_order_id",
                table: "invoice",
                newName: "ServiceOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_service_order_id",
                table: "invoice",
                newName: "IX_invoice_ServiceOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_service_order_ServiceOrderId",
                table: "invoice",
                column: "ServiceOrderId",
                principalTable: "service_order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
