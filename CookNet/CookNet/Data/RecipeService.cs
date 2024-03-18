using Microsoft.EntityFrameworkCore;

namespace CookNet.Data
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<List<RecipeStory>> GetRecipeStoryAsync()
        {
            return await _context.RecipeStories.ToListAsync();
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Instruction>> GetAllInstructionsAsync()
        {
            return await _context.Instructions.ToListAsync();
        }

        public async Task AddIngredientToRecipeAsync(Recipe recipe, string ingredientName, string quantity, string quantityMeasure)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientName);

            if (ingredient == null)
            {
                ingredient = new Ingredient 
                { 
                    Name = ingredientName, 
                    Quantity = quantity, 
                    QuantityUnit = quantityMeasure
                };
                _context.Ingredients.Add(ingredient);
            }

            var recipeIngredient = new RecipeIngredient
            {
                Recipe = recipe,
                Ingredient = ingredient,
                Quantity = quantity,
                QuantityUnit = quantityMeasure
            };

            _context.RecipeIngredients.Add(recipeIngredient);

            await _context.SaveChangesAsync();
        }

        public async Task AddInstructionToRecipeAsync(Recipe recipe, string instructionText)
        {
            var instruction = new Instruction
            {
                StepNumber = recipe.Instructions.Count + 1,
                InstructionText = instructionText
            };

            recipe.Instructions.Add(instruction);

            await _context.SaveChangesAsync();
        }

        public async Task AddRecipeStoryToRecipeAsync(Recipe recipe, string storyText)
        {
            var story = new RecipeStory
            {
                StoryText = storyText
            };
            recipe.RecipeStories.Add(story);

            await _context.SaveChangesAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.ID == recipeId);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe with ID '{recipeId}' not found.");
            }
            return recipe;
        }
    }
}
