using System.ComponentModel.DataAnnotations.Schema;

namespace CookNet.Data
{
    public class RecipeIngredient
    {
        public int ID { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
        public string Quantity { get; set; }
        public string? QuantityUnit { get; set; }
    }
}
