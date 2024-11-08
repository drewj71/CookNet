using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;
using MudBlazor;

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
            try
            {
                return await _context.Recipes.ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving recipes.", ex);
                return new List<Recipe>();
            }
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            try
            {
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                await LogExceptionAsync("An error has occured while creating a new recipe.", ex);
            }
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            try
            {
                var recipe = await _context.Recipes
                    .Include(r => r.RecipeRatings)
                    .FirstOrDefaultAsync(r => r.ID == recipeId);
                if (recipe != null)
                {
                    _context.RecipeRatings.RemoveRange(recipe.RecipeRatings);
                    _context.Recipes.Remove(recipe);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while deleting a recipe.", ex);
            }
        }

        public async Task<List<Ingredient>> GetIngredientsByRecipeIdAsync(int recipeId)
        {
            try
            {
                var recipeIngredients = await _context.RecipeIngredients
                    .Where(ri => ri.RecipeID == recipeId)
                    .Include(ri => ri.Ingredient).ToListAsync();

                var ingredients = recipeIngredients.Select(ri => ri.Ingredient).ToList();

                return ingredients;
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while deleting a recipe.", ex);
                return new List<Ingredient>();
            }
        }

        public async Task<List<Instruction>> GetInstructionsByRecipeIdAsync(int recipeId)
        {
            try
            {
                return await _context.Instructions.Where(ins => ins.RecipeID == recipeId).ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving instructions.", ex);
                return new List<Instruction>();
            }
        }

        public async Task AddIngredientToRecipeAsync(Recipe recipe, string ingredientName, string quantity, string quantityMeasure)
        {
            try
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
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while adding ingredient to recipe.", ex);
            }
        }

        public async Task AddInstructionToRecipeAsync(Recipe recipe, string instructionText)
        {
            try
            {
                var instruction = new Instruction
                {
                    StepNumber = recipe.Instructions.Count + 1,
                    InstructionText = instructionText
                };

                recipe.Instructions.Add(instruction);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while adding instruction to recipe.", ex);
            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            try
            {
                var recipe = await _context.Recipes
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.Instructions)
                    .Include(r => r.RecipeImages)
                    .FirstOrDefaultAsync(r => r.ID == recipeId);
                if (recipe == null)
                {
                    throw new InvalidOperationException($"Recipe with ID '{recipeId}' not found.");
                }
                return recipe;
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while adding instruction to recipe.", ex);
                return new Recipe();
            }
        }

        public async Task<List<Recipe>> GetRecipesByAuthorAsync(string authorId)
        {
            try
            {
                return await _context.Recipes
                    .Where(r => r.AuthorID == authorId)
                    .OrderByDescending(r => r.DateCreated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving recipes.", ex);
                return new List<Recipe>();
            }
        }

        private readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 5242880; // 5 MB

        private async Task<bool> IsValidImage(string fileName)
        {
            try
            {
                var extension = Path.GetExtension(fileName).ToLowerInvariant();
                return AllowedExtensions.Contains(extension);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving recipes.", ex);
                return false;
            }
        }

        public async Task<string> UploadImageAsync(IBrowserFile file)
        {
            try
            {
                var isImageValid = await IsValidImage(file.Name);
                if (file != null && file.Size > 0 && !string.IsNullOrEmpty(file.Name) && isImageValid && file.Size < MaxFileSize)
                {
                    return await SaveImageAsync(file);
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while uploading the image.", ex);
            }
            return "";
        }

        private async Task<string> SaveImageAsync(IBrowserFile file)
        {
            try
            {
                var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.Name)}";
                var filePath = Path.Combine(uploadsDir, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.OpenReadStream(MaxFileSize).CopyToAsync(fileStream);
                }

                return Path.Combine("uploads", uniqueFileName);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while saving the image.", ex);
            }
            return "";
        }

        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while saving changes to the database.", ex);
            }
        }

        public async Task RemoveIngredientFromRecipeAsync(Recipe recipe, Ingredient ingredient)
        {
            try
            {
                var recipeIngredient = await _context.RecipeIngredients.FirstOrDefaultAsync(ri => ri.RecipeID == recipe.ID && ri.IngredientID == ingredient.ID);

                if (recipeIngredient != null)
                {
                    _context.RecipeIngredients.Remove(recipeIngredient);
                    await SaveChanges();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while removing ingredient from recipe.", ex);
            }
        }

        public async Task RemoveInstructionFromRecipeAsync(Recipe recipe, Instruction instruction)
        {
            try
            {
                var recipeInstruction = await _context.Instructions.FirstOrDefaultAsync(i => i.RecipeID == recipe.ID && i.ID == instruction.ID);

                if (recipeInstruction != null)
                {
                    _context.Instructions.Remove(recipeInstruction);
                    await SaveChanges();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while removing instruction from recipe.", ex);
            }
        }

        public async Task<List<Recipe>> GetMostRecentRecipesAsync()
        {
            try
            {
                return await _context.Recipes
                    .Include(r => r.Author)
                    .OrderByDescending(r => r.DateCreated)
                    .Take(10)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving most recent recipes.", ex);
                return new List<Recipe>();
            }
        }

        //public async Task<List<Recipe>> GetTop10RecipesAsync()
        //{

        //}

        public async Task<int> GetCookbookSavesCount(int recipeId)
        {
            try
            {
                return await _context.CookbookRecipes
                    .Where(r => r.RecipeID == recipeId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving cookbook saves count.", ex);
                return -1;
            }
        }

        public async Task<int> GetLikeCountAsync(int recipeId)
        {
            try
            {
                return await _context.RecipeRatings
                    .Where(r => r.RecipeID == recipeId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving recipe likes count.", ex);
                return -1;
            }
        }

        public async Task<bool> IsRecipeLikedByUserAsync(int recipeId, string currentUserId)
        {
            try
            {
                return await _context.RecipeRatings
                .AnyAsync(r => r.RecipeID == recipeId && r.UserID == currentUserId);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving if the user liked the recipe.", ex);
                return false;
            }
        }

        public async Task<int> ToggleLikeAsync(int recipeId, bool isLiked, string currentUserId)
        {
            try
            {
                var existingLike = await _context.RecipeRatings
                    .FirstOrDefaultAsync(r => r.RecipeID == recipeId && r.UserID == currentUserId);

                if (isLiked)
                {
                    if (existingLike == null)
                    {
                        var newLike = new RecipeRating
                        {
                            RecipeID = recipeId,
                            UserID = currentUserId,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.RecipeRatings.Add(newLike);
                        await SaveChanges();
                    }
                }
                else
                {
                    if (existingLike != null)
                    {
                        _context.RecipeRatings.Remove(existingLike);
                        await SaveChanges();
                    }
                }
                return await _context.RecipeRatings.CountAsync(r => r.RecipeID == recipeId);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while processing a like for a user.", ex);
                return -1;
            }
        }

        public async Task<List<RecipeComment>> GetCommentsByRecipeIdAsync(int recipeId)
        {
            try
            {
                return await _context.Comments
                    .Where(c => c.RecipeID == recipeId)
                    .OrderByDescending(c => c.DateCreated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while getting the comments for a recipe.", ex);
                return new List<RecipeComment>();
            }
        }

        public async Task AddCommentAsync(RecipeComment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while adding a comment to a recipe.", ex);
            }
        }

        public async Task<bool> HasRepliesAsync(int commentId)
        {
            try
            {
                return await _context.Comments.AnyAsync(c => c.ParentCommentID == commentId);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while determining if there are replies for this comment.", ex);
                return false;
            }
        }

        public async Task MarkCommentAsDeletedAsync(int commentId)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(commentId);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    await SaveChanges();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while marking this comment as deleted.", ex);
            }
        }

        public async Task UpdateCommentAsync(int commentId, string newCommentText)
        {
            try
            {
                var comment = await _context.Comments
                    .FirstOrDefaultAsync(c => c.CommentID == commentId);

                if (comment == null)
                {
                    throw new InvalidOperationException("Comment not found.");
                }

                comment.Comment = newCommentText;
                comment.DateUpdated = DateTime.UtcNow;

                _context.Comments.Update(comment);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while updating this comment.", ex);
            }
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(commentId);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    await SaveChanges();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while deleting this comment.", ex);
            }
        }

        public async Task<List<RecipeComment>> GetRepliesAsync(int parentCommentId)
        {
            try
            {
                return await _context.Comments
                    .Where(c => c.ParentCommentID == parentCommentId && !c.IsDeleted)
                    .OrderBy(c => c.DateCreated)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the replies to this comment.", ex);
                return new List<RecipeComment>();
            }
        }

        public async Task<PaginatedList<Recipe>> GetPaginatedRecipesBySearchQuery(DateTime startDate, DateTime endDate, string searchQuery, string category, string ethnicity, int pageIndex, int pageSize)
        {
            try
            {
                var query = _context.Recipes
                    .Where(r => r.DateCreated >= startDate && r.DateCreated <= endDate)
                    .Where(r => string.IsNullOrEmpty(searchQuery) ||
                        r.Name.ToLower().Contains(searchQuery.ToLower()) ||
                        r.Description.ToLower().Contains(searchQuery.ToLower()) ||
                        (r.PrepTime + r.CookTime).ToString().Contains(searchQuery))
                    .Where(r => string.IsNullOrEmpty(category) ||
                        r.Category.ToLower().Contains(category.ToLower()))
                    .Where(r => string.IsNullOrEmpty(ethnicity) ||
                        r.Ethnicity.ToLower().Contains(ethnicity.ToLower()));

                var totalRecipes = await query.CountAsync();

                var recipes = await query.Skip((pageIndex - 1) * pageSize)
                                         .Include(r => r.Author)
                                         .OrderByDescending(r => r.DateCreated)
                                         .Take(pageSize)
                                         .ToListAsync();

                return new PaginatedList<Recipe>(recipes, totalRecipes, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving recipes from the search query.", ex);
                return new PaginatedList<Recipe>();
            }
        }

        public async Task<List<RecipeCategory>> GetActiveRecipeCategories()
        {
            try
            {
                return await _context.RecipeCategories
                    .Where(rc => rc.IsActive == true)
                    .OrderBy(rc => rc.CategoryName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the recipe categories.", ex);
                return new List<RecipeCategory>();
            }
        }

        public async Task<List<RecipeEthnicity>> GetActiveRecipeEthnicities()
        {
            try
            {
                return await _context.RecipeEthnicities
                    .Where(re => re.IsActive == true)
                    .OrderBy(re => re.EthnicityName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the recipe ethnicities.", ex);
                return new List<RecipeEthnicity>();
            }
        }

        private async Task LogExceptionAsync(string message, Exception exception)
        {
            var logEntry = new ExceptionLog
            {
                PriorityLevel = DeterminePriority(exception),
                Message = message,
                Exception = exception.ToString(),
                DateLogged = DateTime.Now,
                CreatedBy = "RecipeService"
            };

            _context.Set<ExceptionLog>().Add(logEntry);
            await SaveChanges();
        }

        private string DeterminePriority(Exception exception)
        {
            // Set priority based on exception type or other criteria
            if (exception is ArgumentNullException || exception is NullReferenceException)
                return "HIGH";
            else if (exception is InvalidOperationException)
                return "MED";

            // Default to LOW for unclassified exceptions
            return "LOW";
        }
    }
}
