using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLog_User_UserId",
                schema: "log",
                table: "AuditLog");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketType_User_FKAssignedUserId",
                schema: "issue",
                table: "TicketType");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketWatchList_User_FKUserId",
                schema: "issue",
                table: "TicketWatchList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompany_User_UserId",
                schema: "UserMgmt",
                table: "UserCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartment_User_UserId",
                schema: "UserMgmt",
                table: "UserDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "UserMgmt",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "UserMgmt",
                newName: "Users",
                newSchema: "UserMgmt");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "UserMgmt",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "UserMgmt",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FkUserId = table.Column<int>(type: "integer", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Browser = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    MachineUser = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    LoginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Users_UserId",
                schema: "log",
                table: "AuditLog",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketType_Users_FKAssignedUserId",
                schema: "issue",
                table: "TicketType",
                column: "FKAssignedUserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketWatchList_Users_FKUserId",
                schema: "issue",
                table: "TicketWatchList",
                column: "FKUserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompany_Users_UserId",
                schema: "UserMgmt",
                table: "UserCompany",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartment_Users_UserId",
                schema: "UserMgmt",
                table: "UserDepartment",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLog_Users_UserId",
                schema: "log",
                table: "AuditLog");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketType_Users_FKAssignedUserId",
                schema: "issue",
                table: "TicketType");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketWatchList_Users_FKUserId",
                schema: "issue",
                table: "TicketWatchList");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompany_Users_UserId",
                schema: "UserMgmt",
                table: "UserCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartment_Users_UserId",
                schema: "UserMgmt",
                table: "UserDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "UserMgmt",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "UserMgmt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "UserMgmt",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "UserMgmt",
                newName: "User",
                newSchema: "UserMgmt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "UserMgmt",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_User_UserId",
                schema: "log",
                table: "AuditLog",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketType_User_FKAssignedUserId",
                schema: "issue",
                table: "TicketType",
                column: "FKAssignedUserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketWatchList_User_FKUserId",
                schema: "issue",
                table: "TicketWatchList",
                column: "FKUserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompany_User_UserId",
                schema: "UserMgmt",
                table: "UserCompany",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartment_User_UserId",
                schema: "UserMgmt",
                table: "UserDepartment",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "UserMgmt",
                table: "UserRole",
                column: "UserId",
                principalSchema: "UserMgmt",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
