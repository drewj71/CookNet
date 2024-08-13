using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public string Comment {  get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int? ParentCommentID { get; set; }
        public RecipeComment ParentComment { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<RecipeComment> Replies { get; set; }
    }
}
