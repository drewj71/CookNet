using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeAllergens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllergenID { get; set; }
        public int RecipeID { get; set; }
        public int AllergenTypeID { get; set; }
        public Recipe Recipe { get; set; }
        public AllergenTypes AllergenTypes { get; set; }
    }
}
