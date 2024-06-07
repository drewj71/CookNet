using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class MoreEditingRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEditing",
                table: "RecipeStories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditing",
                table: "Instructions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditing",
                table: "RecipeStories");

            migrationBuilder.DropColumn(
                name: "IsEditing",
                table: "Instructions");
        }
    }
}
