using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class CreateRecipeImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "RecipeImages",
            columns: table => new
            {
                RecipeImageID = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RecipeID = table.Column<int>(nullable: false),
                ImagePathOne = table.Column<string>(nullable: true),
                ImagePathTwo = table.Column<string>(nullable: true),
                ImagePathThree = table.Column<string>(nullable: true),
                ImagePathFour = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RecipeImages", x => x.RecipeImageID);
                table.ForeignKey(
                    name: "FK_RecipeImages_Recipes_RecipeID",
                    column: x => x.RecipeID,
                    principalTable: "Recipes",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "RecipeImages");
        }
    }
}
