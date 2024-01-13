using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Companies",
                newName: "Address");

            migrationBuilder.AddColumn<bool>(
                name: "HasCompany",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCompany",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Companies",
                newName: "Adress");
        }
    }
}
