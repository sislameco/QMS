using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class recoverypasswoardssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "RStatus",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "menu",
                table: "AssociateActionRoutes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "menu",
                table: "AssociateActionRoutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 106);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "timestamp with time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 107);

            migrationBuilder.AddColumn<int>(
                name: "RStatus",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 104);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "menu",
                table: "AssociateActionRoutes",
                type: "timestamp with time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 105);
        }
    }
}
