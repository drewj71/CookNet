using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class LotsOfNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultThumbnails",
                columns: table => new
                {
                    DefaultThumbnailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultThumbnails", x => x.DefaultThumbnailID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeEthnicities",
                columns: table => new
                {
                    EthnicityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EthnicityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEthnicities", x => x.EthnicityID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeNutrition",
                columns: table => new
                {
                    RecipeNutritionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Carbohydrates = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Fats = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Fiber = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Sugar = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Sodium = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ServingSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeNutrition", x => x.RecipeNutritionID);
                    table.ForeignKey(
                        name: "FK_RecipeNutrition_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefaultThumbnails_ImagePath",
                table: "DefaultThumbnails",
                column: "ImagePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_CategoryName",
                table: "RecipeCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEthnicities_EthnicityName",
                table: "RecipeEthnicities",
                column: "EthnicityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNutrition_RecipeID",
                table: "RecipeNutrition",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultThumbnails");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "RecipeEthnicities");

            migrationBuilder.DropTable(
                name: "RecipeNutrition");
        }
    }
}
