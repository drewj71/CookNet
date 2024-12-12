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
        [Column(TypeName = "decimal(10,2)")]
        public decimal Calories { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Protein { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Carbohydrates { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fats { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fiber { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Sugar { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Sodium { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Calcium { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Iron { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal VitaminA {  get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal VitaminC { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cholesterol { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TransFat { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SatFat { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Potassium { get; set; }
        public int ServingSize { get; set; } = 1;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public Recipe Recipe { get; set; }
    }
}
