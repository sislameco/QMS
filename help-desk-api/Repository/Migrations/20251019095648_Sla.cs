using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Sla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "Org",
                table: "SLAConfiguration");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "Org",
                table: "SLAConfiguration",
                newName: "FKTicketTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FKTicketTypeId",
                schema: "Org",
                table: "SLAConfiguration",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "Org",
                table: "SLAConfiguration",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
