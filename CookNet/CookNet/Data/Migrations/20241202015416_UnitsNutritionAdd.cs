using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookNet.Migrations
{
    /// <inheritdoc />
    public partial class UnitsNutritionAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionUnits",
                columns: table => new
                {
                    NutritionUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaloriesUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProteinUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarbsUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatsUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiberUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SugarUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalciumUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SodiumUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IronUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitaminAUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitaminCUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CholesterolUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransFatUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SatFatUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotassiumUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionUnits", x => x.NutritionUnitID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionUnits");
        }
    }
}
