using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeImageID { get; set; }
        public int RecipeID { get; set; }
        public string ImagePathOne { get; set; }
        public string ImagePathTwo { get; set; }
        public string ImagePathThree { get; set; }
        public string ImagePathFour { get; set; }
        public Recipe Recipe { get; set; }
    }
}
