using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class NutritionUnits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NutritionUnitID { get; set; }
        public string CaloriesUnit { get; set; }
        public string ProteinUnit { get; set; }
        public string CarbsUnit { get; set; }
        public string FatsUnit { get; set; }
        public string FiberUnit { get; set; }
        public string SugarUnit { get; set; }
        public string CalciumUnit { get; set; }
        public string SodiumUnit { get; set; }
        public string IronUnit { get; set; }
        public string VitaminAUnit { get; set; }
        public string VitaminCUnit { get; set; }
        public string CholesterolUnit { get; set; }
        public string TransFatUnit { get; set; }
        public string SatFatUnit { get; set; }
        public string PotassiumUnit { get; set; }
    }
}
