using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class PrepTimeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "PrepTime",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrepTime",
                table: "Recipes");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "ID", "Name", "Quantity", "QuantityUnit" },
                values: new object[,]
                {
                    { 1, "Ingredient 1", 0.0, "" },
                    { 2, "Ingredient 2", 0.0, "" }
                });
        }
    }
}
