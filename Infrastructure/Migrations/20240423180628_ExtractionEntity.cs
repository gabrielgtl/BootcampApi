using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtractionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Deposits",
                type: "numeric(20,5)",
                precision: 20,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "Extractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric(20,5)", precision: 20, scale: 5, nullable: false),
                    OperationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Extraction_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extractions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extractions_AccountId",
                table: "Extractions",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extractions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Deposits",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldPrecision: 20,
                oldScale: 5);
        }
    }
}
