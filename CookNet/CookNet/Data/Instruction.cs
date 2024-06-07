namespace CookNet.Data
{
    public class Instruction
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int StepNumber { get; set; }
        public string InstructionText { get; set; }
        public Recipe Recipe { get; set; }
        public bool IsEditing { get; set; } = false;    // For EditRecipe
    }
}
