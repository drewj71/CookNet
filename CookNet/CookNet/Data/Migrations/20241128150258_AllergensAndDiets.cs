using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class AllergensAndDiets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllergenTypes",
                columns: table => new
                {
                    AllergenTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergenTypes", x => x.AllergenTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DietTypes",
                columns: table => new
                {
                    DietTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietTypes", x => x.DietTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    AllergenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    AllergenTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.AllergenID);
                    table.ForeignKey(
                        name: "FK_Allergens_AllergenTypes_AllergenTypeID",
                        column: x => x.AllergenTypeID,
                        principalTable: "AllergenTypes",
                        principalColumn: "AllergenTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergens_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDiet",
                columns: table => new
                {
                    RecipeDietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    DietTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDiet", x => x.RecipeDietID);
                    table.ForeignKey(
                        name: "FK_RecipeDiet_DietTypes_DietTypeID",
                        column: x => x.DietTypeID,
                        principalTable: "DietTypes",
                        principalColumn: "DietTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDiet_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_AllergenTypeID",
                table: "Allergens",
                column: "AllergenTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_RecipeID",
                table: "Allergens",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDiet_DietTypeID",
                table: "RecipeDiet",
                column: "DietTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDiet_RecipeID",
                table: "RecipeDiet",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "RecipeDiet");

            migrationBuilder.DropTable(
                name: "AllergenTypes");

            migrationBuilder.DropTable(
                name: "DietTypes");
        }
    }
}
