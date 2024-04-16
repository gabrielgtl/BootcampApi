using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPromotionEnterprisesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionBusinesses_Enterprises_EnterpriseId",
                table: "PromotionBusinesses");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionBusinesses_Promotions_PromotionId",
                table: "PromotionBusinesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionBusinesses",
                table: "PromotionBusinesses");

            migrationBuilder.RenameTable(
                name: "PromotionBusinesses",
                newName: "PromotionEnterprises");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionBusinesses_EnterpriseId",
                table: "PromotionEnterprises",
                newName: "IX_PromotionEnterprises_EnterpriseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionEnterprises",
                table: "PromotionEnterprises",
                columns: new[] { "PromotionId", "EnterpriseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionEnterprises_Enterprises_EnterpriseId",
                table: "PromotionEnterprises",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionEnterprises_Promotions_PromotionId",
                table: "PromotionEnterprises",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionEnterprises_Enterprises_EnterpriseId",
                table: "PromotionEnterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionEnterprises_Promotions_PromotionId",
                table: "PromotionEnterprises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionEnterprises",
                table: "PromotionEnterprises");

            migrationBuilder.RenameTable(
                name: "PromotionEnterprises",
                newName: "PromotionBusinesses");

            migrationBuilder.RenameIndex(
                name: "IX_PromotionEnterprises_EnterpriseId",
                table: "PromotionBusinesses",
                newName: "IX_PromotionBusinesses_EnterpriseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionBusinesses",
                table: "PromotionBusinesses",
                columns: new[] { "PromotionId", "EnterpriseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionBusinesses_Enterprises_EnterpriseId",
                table: "PromotionBusinesses",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionBusinesses_Promotions_PromotionId",
                table: "PromotionBusinesses",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
