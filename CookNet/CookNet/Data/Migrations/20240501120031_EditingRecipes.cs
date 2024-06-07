using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class EditingRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEditing",
                table: "RecipeIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditing",
                table: "RecipeIngredients");
        }
    }
}
