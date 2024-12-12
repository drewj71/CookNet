using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class AllergenTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllergenTypeID { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeAllergens> Allergens { get; set; }
    }
}
