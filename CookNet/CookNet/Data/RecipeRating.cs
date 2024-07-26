using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }
        public int RecipeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }
        public Recipe Recipe { get; set; }

    }
}
