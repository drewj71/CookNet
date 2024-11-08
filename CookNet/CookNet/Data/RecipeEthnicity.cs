using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class RecipeEthnicity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EthnicityID { get; set; }
        public string EthnicityName { get; set; }
        public bool IsActive { get; set; }
    }
}
