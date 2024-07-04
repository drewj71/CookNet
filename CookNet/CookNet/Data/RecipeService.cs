using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;

namespace CookNet.Data
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipeService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task DeleteRecipeAsync(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe != null)
            {
                //SPROC currently not working
                //await _context.Database.ExecuteSqlRawAsync("EXEC DeleteRelatedIngredients @RecipeID", new SqlParameter("@RecipeID", recipeId));
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId)
        {
            var recipeIngredients = await _context.RecipeIngredients
                .Where(ri => ri.RecipeID == recipeId)
                .Include(ri => ri.Ingredient).ToListAsync();

            var ingredients = recipeIngredients.Select(ri => ri.Ingredient).ToList();

            return ingredients;
        }

        public async Task<List<RecipeStory>> GetRecipeStoryByRecipeIdAsync(int recipeId)
        {
            return await _context.RecipeStories.Where(rs => rs.RecipeID == recipeId).ToListAsync();
        }

        public async Task<List<Instruction>> GetInstructionsByRecipeIdAsync(int recipeId)
        {
            return await _context.Instructions.Where(ins => ins.RecipeID == recipeId).ToListAsync();
        }

        public async Task AddIngredientToRecipeAsync(Recipe recipe, string ingredientName, string quantity, string quantityMeasure)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Quantity = quantity,
                QuantityUnit = quantityMeasure
            };
            _context.Ingredients.Add(ingredient);

            var recipeIngredient = new RecipeIngredient
            {
                Recipe = recipe,
                Ingredient = ingredient,
                Quantity = quantity,
                QuantityUnit = quantityMeasure
            };

            _context.RecipeIngredients.Add(recipeIngredient);
        }

        public async Task AddInstructionToRecipeAsync(Recipe recipe, string instructionText)
        {
            var instruction = new Instruction
            {
                StepNumber = recipe.Instructions.Count + 1,
                InstructionText = instructionText
            };

            recipe.Instructions.Add(instruction);
        }

        public async Task AddRecipeStoryToRecipeAsync(Recipe recipe, string storyText)
        {
            var story = new RecipeStory
            {
                StoryText = storyText
            };
            recipe.RecipeStories.Add(story);
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
        public async Task<List<Recipe>> GetRecipesByAuthorAsync(string authorId)
        {
            return await _context.Recipes
                .Where(r => r.AuthorID == authorId)
                .OrderByDescending(r => r.DateCreated)
                .ToListAsync();
        }

        public async Task<string> UploadImageAsync(IBrowserFile file)
        {
            if (file == null || file.Size == 0 || string.IsNullOrEmpty(file.Name))
            {
                throw new ArgumentException("Invalid file");
            }

            // Save the image and return its path
            return await SaveImageAsync(file);
        }

        private async Task<string> SaveImageAsync(IBrowserFile file)
        {
            // Check if the uploads directory exists, create it if not
            var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            // Generate a unique file name
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.Name)}";
            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            // Save the file to the uploads directory
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(fileStream);
            }

            // Return the relative path to the saved file
            return Path.Combine("uploads", uniqueFileName);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveIngredientFromRecipeAsync(Recipe recipe, Ingredient ingredient)
        {
            var recipeIngredient = await _context.RecipeIngredients.FirstOrDefaultAsync(ri => ri.RecipeID == recipe.ID && ri.IngredientID == ingredient.ID);

            if (recipeIngredient != null)
            {
                _context.RecipeIngredients.Remove(recipeIngredient);
                await SaveChanges();
            }
        }

        public async Task RemoveInstructionFromRecipeAsync(Recipe recipe, Instruction instruction)
        {
            var recipeInstruction = await _context.Instructions.FirstOrDefaultAsync(i => i.RecipeID == recipe.ID && i.ID == instruction.ID);

            if (recipeInstruction != null)
            {
                _context.Instructions.Remove(recipeInstruction);
                await SaveChanges();
            }
        }

        public async Task RemoveStoryFromRecipeAsync(Recipe recipe, RecipeStory story)
        {
            var recipeStory = await _context.RecipeStories.FirstOrDefaultAsync(rs => rs.RecipeID == recipe.ID && rs.RecipeID == story.RecipeID);

            if (recipeStory != null)
            {
                _context.RecipeStories.Remove(recipeStory);
                await SaveChanges();
            }
        }

        public async Task<List<Recipe>> GetMostRecentRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.Author)
                .OrderByDescending(r => r.DateCreated)
                .Take(10)
                .ToListAsync();
        }

        //public async Task<List<Recipe>> GetTop10RecipesAsync()
        //{

        //}
    }
}
