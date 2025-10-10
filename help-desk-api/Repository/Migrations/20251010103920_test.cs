using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FkDepartmentIds",
                schema: "issue",
                table: "TicketType",
                newName: "FKDepartmentIds");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "issue",
                table: "TicketType",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "FKCompanyId",
                schema: "issue",
                table: "TicketType",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKCompanyId",
                schema: "issue",
                table: "TicketType");

            migrationBuilder.RenameColumn(
                name: "FKDepartmentIds",
                schema: "issue",
                table: "TicketType",
                newName: "FkDepartmentIds");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "issue",
                table: "TicketType",
                newName: "Name");
        }
    }
}
