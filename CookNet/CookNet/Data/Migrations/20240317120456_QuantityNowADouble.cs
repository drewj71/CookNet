using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class QuantityNowADouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "RecipeIngredients",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "QuantityUnit",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Ingredients",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 1,
                column: "Quantity",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 2,
                column: "Quantity",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityUnit",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RecipeIngredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Ingredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 1,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "ID",
                keyValue: 2,
                column: "Quantity",
                value: 0);
        }
    }
}
