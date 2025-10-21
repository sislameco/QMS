using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Ticket_FKCompanyId",
                schema: "issue",
                table: "Ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Companies_FKCompanyId",
                schema: "issue",
                table: "Ticket",
                column: "FKCompanyId",
                principalSchema: "Org",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Companies_FKCompanyId",
                schema: "issue",
                table: "Ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Ticket_FKCompanyId",
                schema: "issue",
                table: "Ticket",
                column: "FKCompanyId",
                principalSchema: "issue",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
