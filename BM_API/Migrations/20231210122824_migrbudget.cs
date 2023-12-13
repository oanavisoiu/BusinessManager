using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_API.Migrations
{
    /// <inheritdoc />
    public partial class migrbudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_BudgetTypes_BudgetTypeId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "BudgetTypeId",
                table: "Budgets",
                newName: "PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_BudgetTypeId",
                table: "Budgets",
                newName: "IX_Budgets_PaymentTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Budgets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "FK_Budgets_PaymentTypes_PaymentTypeId",
                table: "Budgets",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
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
                name: "FK_Budgets_PaymentTypes_PaymentTypeId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CompanyId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "Budgets",
                newName: "BudgetTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_PaymentTypeId",
                table: "Budgets",
                newName: "IX_Budgets_BudgetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_BudgetTypes_BudgetTypeId",
                table: "Budgets",
                column: "BudgetTypeId",
                principalTable: "BudgetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
