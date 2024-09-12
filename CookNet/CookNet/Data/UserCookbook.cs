using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace CookNet.Data
{
    public class UserCookbook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CookbookID { get; set; }
        public string UserID { get; set; }
        [Required] public string CookbookName { get; set; }
        public string? CookbookDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? CoverImage { get; set; }
        public bool IsPublic { get; set; } = false;

        public ICollection<CookbookRecipe> CookbookRecipes { get; set; }
        public ApplicationUser User { get; set; }

        public UserCookbook()
        {
            CookbookRecipes = new List<CookbookRecipe>();
        }
    }
}
