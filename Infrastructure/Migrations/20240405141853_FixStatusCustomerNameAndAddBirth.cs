using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusCustomerNameAndAddBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Customers",
                newName: "CustomerStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerStatus",
                table: "Customers",
                newName: "Status");
        }
    }
}
