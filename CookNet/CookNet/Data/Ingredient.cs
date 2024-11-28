using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? QuantityUnit { get; set; }
        public double? ToGrams { get; set; }
    }
}
