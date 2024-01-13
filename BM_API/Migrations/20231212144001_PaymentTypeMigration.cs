using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_API.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Companies_CompanyId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CompanyId",
                table: "Budgets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CompanyId",
                table: "Budgets",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Companies_CompanyId",
                table: "Budgets",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
