using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Bank_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BuyValue = table.Column<decimal>(type: "numeric", nullable: false),
                    SellValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Lastname = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DocumentNumber = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Status = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Customer_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Holder = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Account_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationalLimit = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: true),
                    MonthAverage = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: true),
                    Interest = table.Column<decimal>(type: "numeric(10,5)", precision: 10, scale: 5, nullable: true),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CurrentAccount_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Destination = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    TransferredDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TransferStatus = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Movimientos_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movements_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SavingType = table.Column<int>(type: "integer", nullable: false),
                    HolderName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SavingAccount_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_AccountId",
                table: "CurrentAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankId",
                table: "Customers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_AccountId",
                table: "Movements",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentAccounts");

            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropTable(
                name: "SavingAccounts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
