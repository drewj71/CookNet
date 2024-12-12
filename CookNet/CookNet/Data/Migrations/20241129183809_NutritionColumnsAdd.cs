using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class NutritionColumnsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServingSize",
                table: "RecipeNutrition",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Calcium",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cholesterol",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Iron",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Potassium",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SatFat",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransFat",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VitaminA",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VitaminC",
                table: "RecipeNutrition",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calcium",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "Cholesterol",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "Iron",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "Potassium",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "SatFat",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "TransFat",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "VitaminA",
                table: "RecipeNutrition");

            migrationBuilder.DropColumn(
                name: "VitaminC",
                table: "RecipeNutrition");

            migrationBuilder.AlterColumn<string>(
                name: "ServingSize",
                table: "RecipeNutrition",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
