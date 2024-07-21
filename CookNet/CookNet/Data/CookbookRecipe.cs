using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class CookbookRecipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CookbookRecipeID { get; set; }
        public int CookbookID { get; set; }
        public int RecipeID { get; set; }
        public DateTime DateAdded { get; set; }

        public UserCookbook UserCookbook { get; set; }
        public Recipe Recipe { get; set; }
    }
}
