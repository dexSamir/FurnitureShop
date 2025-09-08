using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_IsPrimary",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsPrimary",
                table: "Categories",
                column: "IsPrimary");
        }
    }
}
