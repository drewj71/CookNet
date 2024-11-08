using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class ExceptionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExceptionID { get; set; }
        public string PriorityLevel { get; set; }
        public string Message { get; set; }
        public string Exception {  get; set; }
        public DateTime DateLogged { get; set; }
        public string CreatedBy { get; set; }
    }
}
