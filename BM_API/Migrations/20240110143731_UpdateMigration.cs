using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ToDos_CompanyId",
                table: "ToDos",
                column: "CompanyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Companies_CompanyId",
                table: "ToDos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Companies_CompanyId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Companies_CompanyId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_CompanyId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CompanyId",
                table: "Budgets");
        }
    }
}
