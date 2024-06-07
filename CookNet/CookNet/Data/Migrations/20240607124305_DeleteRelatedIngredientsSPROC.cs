using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRelatedIngredientsSPROC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteRelatedIngredients
                    @RecipeID INT
                AS
                BEGIN 
                    DELETE FROM Ingredients
                    WHERE ID in (
                        SELECT i.ID
                        FROM Ingredients i
                        LEFT JOIN RecipeIngredients ri
                        ON i.ID = ri.IngredientID
                        WHERE ri.RecipeID = @RecipeID
                    );
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE DeleteRelatedIngredients;");
        }
    }
}
