using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class ManyRecipeImagesAndRecipeImageID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "RecipeImages",
                newName: "ImagePathTwo");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathFour",
                table: "RecipeImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathOne",
                table: "RecipeImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePathThree",
                table: "RecipeImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathFour",
                table: "RecipeImages");

            migrationBuilder.DropColumn(
                name: "ImagePathOne",
                table: "RecipeImages");

            migrationBuilder.DropColumn(
                name: "ImagePathThree",
                table: "RecipeImages");

            migrationBuilder.RenameColumn(
                name: "ImagePathTwo",
                table: "RecipeImages",
                newName: "ImagePath");
        }
    }
}
