using CookNet.Data;

public class RecipeIngredient
{
    public int RecipeID { get; set; }
    public int IngredientID { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public Recipe Recipe { get; set; }
    public Ingredient Ingredient { get; set; }

    // GetHashCode and Equals overrides
    public override int GetHashCode()
    {
        return HashCode.Combine(RecipeID, IngredientID);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        RecipeIngredient other = (RecipeIngredient)obj;
        return RecipeID == other.RecipeID && IngredientID == other.IngredientID;
    }
}
