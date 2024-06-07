using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeStory
    {
        [Key]
        public int RecipeID { get; set; }
        public string StoryText { get; set; }
        public Recipe Recipe { get; set; }
        public bool IsEditing { get; set; } = false;    // For EditRecipe

    }
}
