using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class isCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkUserId",
                schema: "issue",
                table: "TicketLink",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                schema: "issue",
                table: "Ticket",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketLink_FkUserId",
                schema: "issue",
                table: "TicketLink",
                column: "FkUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketLink_Users_FkUserId",
                schema: "issue",
                table: "TicketLink",
                column: "FkUserId",
                principalSchema: "UserMgmt",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketLink_Users_FkUserId",
                schema: "issue",
                table: "TicketLink");

            migrationBuilder.DropIndex(
                name: "IX_TicketLink_FkUserId",
                schema: "issue",
                table: "TicketLink");

            migrationBuilder.DropColumn(
                name: "FkUserId",
                schema: "issue",
                table: "TicketLink");

            migrationBuilder.DropColumn(
                name: "IsCustomer",
                schema: "issue",
                table: "Ticket");
        }
    }
}
