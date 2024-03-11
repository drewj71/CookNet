using Microsoft.EntityFrameworkCore;

namespace CookNet.Data
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task AddIngredientToRecipeAsync(Recipe recipe, string ingredientName, int quantity)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientName);

            if (ingredient == null)
            {
                ingredient = new Ingredient { Name = ingredientName };
                _context.Ingredients.Add(ingredient);
            }

            var recipeIngredient = new RecipeIngredient
            {
                Recipe = recipe,
                Ingredient = ingredient,
                Quantity = quantity
            };

            _context.RecipeIngredients.Add(recipeIngredient);

            await _context.SaveChangesAsync();
        }
    }
}
