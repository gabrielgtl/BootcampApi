using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditCardTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cvv = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    CreditCardStatus = table.Column<int>(type: "integer", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    AvailableCredit = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    CurrentDebt = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CreditCard_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ClientId",
                table: "CreditCards",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CurrencyId",
                table: "CreditCards",
                column: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
