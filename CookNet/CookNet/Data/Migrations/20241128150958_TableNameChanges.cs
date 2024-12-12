using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class TableNameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_AllergenTypes_AllergenTypeID",
                table: "Allergens");

            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_Recipes_RecipeID",
                table: "Allergens");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiet_DietTypes_DietTypeID",
                table: "RecipeDiet");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiet_Recipes_RecipeID",
                table: "RecipeDiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDiet",
                table: "RecipeDiet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens");

            migrationBuilder.RenameTable(
                name: "RecipeDiet",
                newName: "RecipeDiets");

            migrationBuilder.RenameTable(
                name: "Allergens",
                newName: "RecipeAllergens");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDiet_RecipeID",
                table: "RecipeDiets",
                newName: "IX_RecipeDiets_RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDiet_DietTypeID",
                table: "RecipeDiets",
                newName: "IX_RecipeDiets_DietTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Allergens_RecipeID",
                table: "RecipeAllergens",
                newName: "IX_RecipeAllergens_RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_Allergens_AllergenTypeID",
                table: "RecipeAllergens",
                newName: "IX_RecipeAllergens_AllergenTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDiets",
                table: "RecipeDiets",
                column: "RecipeDietID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeAllergens",
                table: "RecipeAllergens",
                column: "AllergenID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeAllergens_AllergenTypes_AllergenTypeID",
                table: "RecipeAllergens",
                column: "AllergenTypeID",
                principalTable: "AllergenTypes",
                principalColumn: "AllergenTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeAllergens_Recipes_RecipeID",
                table: "RecipeAllergens",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiets_DietTypes_DietTypeID",
                table: "RecipeDiets",
                column: "DietTypeID",
                principalTable: "DietTypes",
                principalColumn: "DietTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiets_Recipes_RecipeID",
                table: "RecipeDiets",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAllergens_AllergenTypes_AllergenTypeID",
                table: "RecipeAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAllergens_Recipes_RecipeID",
                table: "RecipeAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiets_DietTypes_DietTypeID",
                table: "RecipeDiets");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDiets_Recipes_RecipeID",
                table: "RecipeDiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeDiets",
                table: "RecipeDiets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeAllergens",
                table: "RecipeAllergens");

            migrationBuilder.RenameTable(
                name: "RecipeDiets",
                newName: "RecipeDiet");

            migrationBuilder.RenameTable(
                name: "RecipeAllergens",
                newName: "Allergens");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDiets_RecipeID",
                table: "RecipeDiet",
                newName: "IX_RecipeDiet_RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDiets_DietTypeID",
                table: "RecipeDiet",
                newName: "IX_RecipeDiet_DietTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeAllergens_RecipeID",
                table: "Allergens",
                newName: "IX_Allergens_RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeAllergens_AllergenTypeID",
                table: "Allergens",
                newName: "IX_Allergens_AllergenTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeDiet",
                table: "RecipeDiet",
                column: "RecipeDietID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens",
                column: "AllergenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_AllergenTypes_AllergenTypeID",
                table: "Allergens",
                column: "AllergenTypeID",
                principalTable: "AllergenTypes",
                principalColumn: "AllergenTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_Recipes_RecipeID",
                table: "Allergens",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiet_DietTypes_DietTypeID",
                table: "RecipeDiet",
                column: "DietTypeID",
                principalTable: "DietTypes",
                principalColumn: "DietTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDiet_Recipes_RecipeID",
                table: "RecipeDiet",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
