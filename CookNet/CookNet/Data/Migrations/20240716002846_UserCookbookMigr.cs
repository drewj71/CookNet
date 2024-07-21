using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class UserCookbookMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeImageID",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "UserCookbooks",
                columns: table => new
                {
                    CookbookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CookbookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookbookDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCookbooks", x => x.CookbookID);
                    table.ForeignKey(
                        name: "FK_UserCookbooks_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CookbookRecipes",
                columns: table => new
                {
                    CookbookRecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CookbookID = table.Column<int>(type: "int", nullable: false),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookbookRecipes", x => x.CookbookRecipeID);
                    table.ForeignKey(
                        name: "FK_CookbookRecipes_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookbookRecipes_UserCookbooks_CookbookID",
                        column: x => x.CookbookID,
                        principalTable: "UserCookbooks",
                        principalColumn: "CookbookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookbookRecipes_CookbookID",
                table: "CookbookRecipes",
                column: "CookbookID");

            migrationBuilder.CreateIndex(
                name: "IX_CookbookRecipes_RecipeID",
                table: "CookbookRecipes",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCookbooks_UserID",
                table: "UserCookbooks",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookbookRecipes");

            migrationBuilder.DropTable(
                name: "UserCookbooks");

            migrationBuilder.AddColumn<int>(
                name: "RecipeImageID",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
