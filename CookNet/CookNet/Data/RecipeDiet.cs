using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeDiet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeDietID { get; set; }
        public int RecipeID { get; set; }
        public int DietTypeID { get; set; }
        public Recipe Recipe { get; set; }
        public DietTypes DietTypes { get; set; }
    }
}
