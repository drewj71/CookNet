using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class DietTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DietTypeID { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeDiet> RecipeDiets { get; set; }
    }
}
