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
    public class CookbookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DbContextFactory _contextFactory;

        public CookbookService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, DbContextFactory contextFactory)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _contextFactory = contextFactory;
        }

        public async Task<List<UserCookbook>> GetCookbooksAsync()
        {
            try
            {
                return await _context.UserCookbooks.ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the cookbooks.", ex);
                return new List<UserCookbook>();
            }
        }

        public async Task<UserCookbook?> GetCookbookByIdAsync(int cookbookID)
        {
            try
            {
                return await _context.UserCookbooks.FindAsync(cookbookID);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the cookbook.", ex);
                return new UserCookbook();
            }
        }

        public async Task<List<UserCookbook>> GetCookbooksByUserIdAsync(string userId)
        {
            try
            {
                return await _context.UserCookbooks
                    .Where(c => c.UserID == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the cookbooks.", ex);
                return new List<UserCookbook>();
            }
        }

        public async Task<List<UserCookbook>> GetCookbooksContainingRecipeAsync(int recipeId)
        {
            try
            {
                return await _context.UserCookbooks
                                       .Where(c => c.CookbookRecipes.Any(cr => cr.RecipeID == recipeId))
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the cookbooks.", ex);
                return new List<UserCookbook>();
            }
        }

        public async Task<List<Recipe>> GetRecipesByCookbookIdAsync(int cookbookId)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var recipes = await context.Recipes
                        .Where(r => r.CookbookRecipes.Any(cr => cr.CookbookID == cookbookId))
                        .Select(r => new Recipe
                        {
                            ID = r.ID,
                            Name = r.Name,
                            DateCreated = r.DateCreated,
                            CookTime = r.CookTime,
                            PrepTime = r.PrepTime,
                            ThumbnailImage = r.ThumbnailImage,
                            Author = r.Author
                        })
                        .ToListAsync();

                    return recipes;
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while retrieving the recipes for a cookbook.", ex);
                return new List<Recipe>();
            }
        }

        public async Task CreateCookbookAsync(UserCookbook book)
        {
            try
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book));
                }
                _context.UserCookbooks.Add(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while creating a cookbook.", ex);
            }
        }

        public async Task UpdateCookbookAsync(UserCookbook book)
        {
            try
            {
                var existingBook = await _context.UserCookbooks.FindAsync(book.CookbookID);
                if (existingBook != null)
                {
                    _context.Entry(existingBook).CurrentValues.SetValues(book);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while updating a cookbook.", ex);
            }
        }

        public async Task DeleteCookbookAsync(int cookbookID)
        {
            try
            {
                var recipesInBook = _context.CookbookRecipes.Where(cr => cr.CookbookID == cookbookID);
                if (recipesInBook.Any())
                {
                    await RemoveCookbookRecipes(recipesInBook);
                }

                var book = await _context.UserCookbooks.FindAsync(cookbookID);
                _context.UserCookbooks.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while deleting a cookbook.", ex);
            }
        }

        public async Task RemoveCookbookRecipes(IEnumerable<CookbookRecipe> recipes)
        {
            try
            {
                _context.CookbookRecipes.RemoveRange(recipes);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while removing recipes from cookbook.", ex);
            }
        }

        public async Task AddRecipeToCookbooks(int recipeId, List<int> cookbookIds)
        {
            try
            {
                var existingAssociations = _context.CookbookRecipes
                    .Where(cr => cr.RecipeID == recipeId)
                    .ToList();

                var newAssociations = cookbookIds
                    .Where(cookbookId => !existingAssociations.Any(cr => cr.CookbookID == cookbookId))
                    .Select(cookbookId => new CookbookRecipe
                    {
                        CookbookID = cookbookId,
                        RecipeID = recipeId,
                        DateAdded = DateTime.UtcNow
                    });

                _context.CookbookRecipes.AddRange(newAssociations);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while adding a recipe to cookbook.", ex);
            }
        }

        public async Task RemoveRecipeFromCookbooks(int recipeId, List<int> cookbookIds)
        {
            try
            {
                var cookbookRecipesToRemove = await _context.CookbookRecipes
                    .Where(cr => cookbookIds.Contains(cr.CookbookID) && cr.RecipeID == recipeId)
                    .ToListAsync();

                _context.CookbookRecipes.RemoveRange(cookbookRecipesToRemove);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await LogExceptionAsync("An error has occured while removing a recipe from cookbook.", ex);
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

        private async Task LogExceptionAsync(string message, Exception exception)
        {
            var logEntry = new ExceptionLog
            {
                PriorityLevel = DeterminePriority(exception),
                Message = message,
                Exception = exception.ToString(),
                DateLogged = DateTime.Now,
                CreatedBy = "CookbookService"
            };

            _context.Set<ExceptionLog>().Add(logEntry);
            await _context.SaveChangesAsync();
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
