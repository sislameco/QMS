using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_FKRoleId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_FKUserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_FKRoleId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKRoleId",
                principalSchema: "UserMgmt",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_FKUserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "FKUserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_FKRoleId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_FKUserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_FKRoleId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_FKUserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "UserMgmt",
                table: "UserRole",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "UserMgmt",
                table: "UserRole",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "UserMgmt",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
