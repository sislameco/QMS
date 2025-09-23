using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionMap_MenuAction_MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionMap_Menu_MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionRoleMapping_MenuActionMap_MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionRoleMapping_Role_RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropTable(
                name: "RoleCompany",
                schema: "UserMgmt");

            migrationBuilder.DropTable(
                name: "RoleDepartment",
                schema: "UserMgmt");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionRoleMapping_MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionRoleMapping_RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionMap_MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionMap_MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "RStatus",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "log",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropColumn(
                name: "MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropColumn(
                name: "MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                schema: "UserMgmt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPasswordChange",
                schema: "UserMgmt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApiUrl",
                schema: "UserMgmt",
                table: "MenuActionMap",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssociateActionRoutes",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkMenuActionMapId = table.Column<long>(type: "bigint", nullable: true),
                    ApiUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HttpVerb = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociateActionRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociateActionRoutes_MenuActionMap_FkMenuActionMapId",
                        column: x => x.FkMenuActionMapId,
                        principalSchema: "UserMgmt",
                        principalTable: "MenuActionMap",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FkUserId = table.Column<long>(type: "bigint", nullable: false),
                    UserIp = table.Column<string>(type: "text", nullable: true),
                    FkLoginId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDepartments",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    FkUserId = table.Column<long>(type: "bigint", nullable: false),
                    FKDepartmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDepartments_Companies_FkCompanyId",
                        column: x => x.FkCompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartments_Department_FKDepartmentId",
                        column: x => x.FKDepartmentId,
                        principalSchema: "Org",
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartments_Users_FkUserId",
                        column: x => x.FkUserId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_FKMenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "FKMenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_FKRoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "FKRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_FKMenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "FKMenuActionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_FKMenuId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "FKMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociateActionRoutes_FkMenuActionMapId",
                table: "AssociateActionRoutes",
                column: "FkMenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_FkCompanyId",
                schema: "UserMgmt",
                table: "UserDepartments",
                column: "FkCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_FKDepartmentId",
                schema: "UserMgmt",
                table: "UserDepartments",
                column: "FKDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_FkUserId",
                schema: "UserMgmt",
                table: "UserDepartments",
                column: "FkUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionMap_MenuAction_FKMenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "FKMenuActionId",
                principalSchema: "UserMgmt",
                principalTable: "MenuAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionMap_Menu_FKMenuId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "FKMenuId",
                principalSchema: "UserMgmt",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionRoleMapping_MenuActionMap_FKMenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "FKMenuActionMapId",
                principalSchema: "UserMgmt",
                principalTable: "MenuActionMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionRoleMapping_Role_FKRoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "FKRoleId",
                principalSchema: "UserMgmt",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionMap_MenuAction_FKMenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionMap_Menu_FKMenuId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionRoleMapping_MenuActionMap_FKMenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuActionRoleMapping_Role_FKRoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropTable(
                name: "AssociateActionRoutes");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "log");

            migrationBuilder.DropTable(
                name: "UserDepartments",
                schema: "UserMgmt");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionRoleMapping_FKMenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionRoleMapping_FKRoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionMap_FKMenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropIndex(
                name: "IX_MenuActionMap_FKMenuId",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.DropColumn(
                name: "LastPasswordChange",
                schema: "UserMgmt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApiUrl",
                schema: "UserMgmt",
                table: "MenuActionMap");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                schema: "UserMgmt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "log",
                table: "UserLogins",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "log",
                table: "UserLogins",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                schema: "log",
                table: "UserLogins",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 106);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "log",
                table: "UserLogins",
                type: "timestamp with time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 107);

            migrationBuilder.AddColumn<int>(
                name: "RStatus",
                schema: "log",
                table: "UserLogins",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "log",
                table: "UserLogins",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 104);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "log",
                table: "UserLogins",
                type: "timestamp with time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 105);

            migrationBuilder.AddColumn<long>(
                name: "MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleCompany",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    FKCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    FKRoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleCompany_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Org",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleCompany_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserMgmt",
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleCompany_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleDepartment",
                schema: "UserMgmt",
                columns: table => new
                {
                    RStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    FKDepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    FKRoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleDepartment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Org",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleDepartment_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "UserMgmt",
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleDepartment_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalSchema: "UserMgmt",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "MenuActionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionRoleMapping_RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "MenuActionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuActionMap_MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_CompanyId",
                schema: "UserMgmt",
                table: "RoleCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_RoleId",
                schema: "UserMgmt",
                table: "RoleCompany",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_UserModelId",
                schema: "UserMgmt",
                table: "RoleCompany",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDepartment_DepartmentId",
                schema: "UserMgmt",
                table: "RoleDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDepartment_RoleId",
                schema: "UserMgmt",
                table: "RoleDepartment",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDepartment_UserModelId",
                schema: "UserMgmt",
                table: "RoleDepartment",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionMap_MenuAction_MenuActionId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "MenuActionId",
                principalSchema: "UserMgmt",
                principalTable: "MenuAction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionMap_Menu_MenuId",
                schema: "UserMgmt",
                table: "MenuActionMap",
                column: "MenuId",
                principalSchema: "UserMgmt",
                principalTable: "Menu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionRoleMapping_MenuActionMap_MenuActionMapId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "MenuActionMapId",
                principalSchema: "UserMgmt",
                principalTable: "MenuActionMap",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuActionRoleMapping_Role_RoleId",
                schema: "UserMgmt",
                table: "MenuActionRoleMapping",
                column: "RoleId",
                principalSchema: "UserMgmt",
                principalTable: "Role",
                principalColumn: "Id");
        }
    }
}
