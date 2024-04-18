using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovementConfigUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Accounts_AccountId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_AccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Movements",
                newName: "OriginAccountId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequestDescription",
                table: "Requests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferredDateTime",
                table: "Movements",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movements",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DestinationAccountId",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovementType",
                table: "Movements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements",
                column: "DestinationAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Customers_CustomerId",
                table: "Requests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Accounts_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Customers_CustomerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Movements_DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestDescription",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "DestinationAccountId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "OriginAccountId",
                table: "Movements",
                newName: "AccountId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Requests",
                type: "numeric(10,5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Requests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Term",
                table: "Requests",
                type: "numeric(20,5)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferredDateTime",
                table: "Movements",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Movements",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_AccountId",
                table: "Movements",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Accounts_AccountId",
                table: "Movements",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
