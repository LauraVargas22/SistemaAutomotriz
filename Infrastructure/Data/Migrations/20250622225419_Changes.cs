using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditories_Users_UserId",
                table: "auditories");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailsDiagnostics_diagnostics_DiagnosticId",
                table: "DetailsDiagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailsDiagnostics_service_order_ServiceOrderId",
                table: "DetailsDiagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_diagnostics_Users_user_id",
                table: "diagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_Users_UserMemberId",
                table: "refresh_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUser_Roles_RolsId",
                table: "RolUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUser_Users_UsersId",
                table: "RolUser");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_Users_UserId",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_Vehicles_vehicles_id",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_clients_client_id",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RolId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Users_UserId",
                table: "UserSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_clients_client_id",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailsDiagnostics",
                table: "DetailsDiagnostics");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "vehicles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Specializations",
                newName: "specializations");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "UserSpecializations",
                newName: "user_specializations");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "DetailsDiagnostics",
                newName: "details_diagnostics");

            migrationBuilder.RenameColumn(
                name: "VIN",
                table: "vehicles",
                newName: "vin");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "vehicles",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "vehicles",
                newName: "mileage");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "vehicles",
                newName: "brand");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VIN",
                table: "vehicles",
                newName: "IX_vehicles_vin");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_client_id",
                table: "vehicles",
                newName: "IX_vehicles_client_id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserName",
                table: "users",
                newName: "IX_users_username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "users",
                newName: "IX_users_email");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "type_service",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "type_service",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "type_service",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "state",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "specializations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "spare_part",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "spare_part",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "spare_part",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "spare_part",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "spare_part",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "MiniStock",
                table: "spare_part",
                newName: "min_stock");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "service_order",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_service_order_client_id",
                table: "service_order",
                newName: "IX_service_order_ClientId");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "clients",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "clients",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "clients",
                newName: "identification");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "clients",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Birth",
                table: "clients",
                newName: "birth");

            migrationBuilder.RenameIndex(
                name: "IX_clients_Identification",
                table: "clients",
                newName: "IX_clients_identification");

            migrationBuilder.RenameColumn(
                name: "Entity",
                table: "auditories",
                newName: "entity");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "auditories",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TypeAction",
                table: "auditories",
                newName: "type_action");

            migrationBuilder.RenameIndex(
                name: "IX_auditories_UserId",
                table: "auditories",
                newName: "IX_auditories_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecializations_UserId",
                table: "user_specializations",
                newName: "IX_user_specializations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RolId",
                table: "user_roles",
                newName: "IX_user_roles_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_DetailsDiagnostics_DiagnosticId",
                table: "details_diagnostics",
                newName: "IX_details_diagnostics_DiagnosticId");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "service_order",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicles",
                table: "vehicles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_specializations",
                table: "specializations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_specializations",
                table: "user_specializations",
                columns: new[] { "SpecializationId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "UserId", "RolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_details_diagnostics",
                table: "details_diagnostics",
                columns: new[] { "ServiceOrderId", "DiagnosticId" });

            migrationBuilder.AddForeignKey(
                name: "FK_auditories_users_user_id",
                table: "auditories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_details_diagnostics_diagnostics_DiagnosticId",
                table: "details_diagnostics",
                column: "DiagnosticId",
                principalTable: "diagnostics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_details_diagnostics_service_order_ServiceOrderId",
                table: "details_diagnostics",
                column: "ServiceOrderId",
                principalTable: "service_order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_diagnostics_users_user_id",
                table: "diagnostics",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_UserMemberId",
                table: "refresh_tokens",
                column: "UserMemberId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUser_roles_RolsId",
                table: "RolUser",
                column: "RolsId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUser_users_UsersId",
                table: "RolUser",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_clients_ClientId",
                table: "service_order",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_users_UserId",
                table: "service_order",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_vehicles_vehicles_id",
                table: "service_order",
                column: "vehicles_id",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_RolId",
                table: "user_roles",
                column: "RolId",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_UserId",
                table: "user_roles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_specializations_specializations_SpecializationId",
                table: "user_specializations",
                column: "SpecializationId",
                principalTable: "specializations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_specializations_users_UserId",
                table: "user_specializations",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_clients_client_id",
                table: "vehicles",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditories_users_user_id",
                table: "auditories");

            migrationBuilder.DropForeignKey(
                name: "FK_details_diagnostics_diagnostics_DiagnosticId",
                table: "details_diagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_details_diagnostics_service_order_ServiceOrderId",
                table: "details_diagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_diagnostics_users_user_id",
                table: "diagnostics");

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_UserMemberId",
                table: "refresh_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUser_roles_RolsId",
                table: "RolUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RolUser_users_UsersId",
                table: "RolUser");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_clients_ClientId",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_users_UserId",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_vehicles_vehicles_id",
                table: "service_order");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_RolId",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_UserId",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_specializations_specializations_SpecializationId",
                table: "user_specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_user_specializations_users_UserId",
                table: "user_specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_clients_client_id",
                table: "vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicles",
                table: "vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_specializations",
                table: "specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_specializations",
                table: "user_specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_details_diagnostics",
                table: "details_diagnostics");

            migrationBuilder.RenameTable(
                name: "vehicles",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "specializations",
                newName: "Specializations");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "user_specializations",
                newName: "UserSpecializations");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "details_diagnostics",
                newName: "DetailsDiagnostics");

            migrationBuilder.RenameColumn(
                name: "vin",
                table: "Vehicles",
                newName: "VIN");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Vehicles",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "mileage",
                table: "Vehicles",
                newName: "Mileage");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "Vehicles",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_vin",
                table: "Vehicles",
                newName: "IX_Vehicles_VIN");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_client_id",
                table: "Vehicles",
                newName: "IX_Vehicles_client_id");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "Users",
                newName: "IX_Users_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_users_email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "type_service",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "type_service",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "type_service",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "state",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Specializations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "spare_part",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "spare_part",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "spare_part",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "spare_part",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "spare_part",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "min_stock",
                table: "spare_part",
                newName: "MiniStock");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "service_order",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "IX_service_order_ClientId",
                table: "service_order",
                newName: "IX_service_order_client_id");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "clients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "clients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "identification",
                table: "clients",
                newName: "Identification");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "clients",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "birth",
                table: "clients",
                newName: "Birth");

            migrationBuilder.RenameIndex(
                name: "IX_clients_identification",
                table: "clients",
                newName: "IX_clients_Identification");

            migrationBuilder.RenameColumn(
                name: "entity",
                table: "auditories",
                newName: "Entity");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "auditories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "type_action",
                table: "auditories",
                newName: "TypeAction");

            migrationBuilder.RenameIndex(
                name: "IX_auditories_user_id",
                table: "auditories",
                newName: "IX_auditories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_specializations_UserId",
                table: "UserSpecializations",
                newName: "IX_UserSpecializations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_RolId",
                table: "UserRoles",
                newName: "IX_UserRoles_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_details_diagnostics_DiagnosticId",
                table: "DetailsDiagnostics",
                newName: "IX_DetailsDiagnostics_DiagnosticId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "service_order",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations",
                columns: new[] { "SpecializationId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailsDiagnostics",
                table: "DetailsDiagnostics",
                columns: new[] { "ServiceOrderId", "DiagnosticId" });

            migrationBuilder.AddForeignKey(
                name: "FK_auditories_Users_UserId",
                table: "auditories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailsDiagnostics_diagnostics_DiagnosticId",
                table: "DetailsDiagnostics",
                column: "DiagnosticId",
                principalTable: "diagnostics",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailsDiagnostics_service_order_ServiceOrderId",
                table: "DetailsDiagnostics",
                column: "ServiceOrderId",
                principalTable: "service_order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_diagnostics_Users_user_id",
                table: "diagnostics",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_Users_UserMemberId",
                table: "refresh_tokens",
                column: "UserMemberId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUser_Roles_RolsId",
                table: "RolUser",
                column: "RolsId",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUser_Users_UsersId",
                table: "RolUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_Users_UserId",
                table: "service_order",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_Vehicles_vehicles_id",
                table: "service_order",
                column: "vehicles_id",
                principalTable: "Vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_clients_client_id",
                table: "service_order",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RolId",
                table: "UserRoles",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Users_UserId",
                table: "UserSpecializations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_clients_client_id",
                table: "Vehicles",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
