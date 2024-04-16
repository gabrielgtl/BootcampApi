using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEnterpriseName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionBusinesses_Bussineses_EnterpriseId",
                table: "PromotionBusinesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bussineses",
                table: "Bussineses");

            migrationBuilder.RenameTable(
                name: "Bussineses",
                newName: "Enterprises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionBusinesses_Enterprises_EnterpriseId",
                table: "PromotionBusinesses",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionBusinesses_Enterprises_EnterpriseId",
                table: "PromotionBusinesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises");

            migrationBuilder.RenameTable(
                name: "Enterprises",
                newName: "Bussineses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bussineses",
                table: "Bussineses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionBusinesses_Bussineses_EnterpriseId",
                table: "PromotionBusinesses",
                column: "EnterpriseId",
                principalTable: "Bussineses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
