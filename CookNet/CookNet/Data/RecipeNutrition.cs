using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeNutrition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeNutritionID { get; set; }
        public int RecipeID { get; set; }
        public int Calories { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Protein { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Carbohydrates { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Fats { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Fiber { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Sugar { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Sodium { get; set; }
        public string ServingSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public Recipe Recipe { get; set; }
    }
}
