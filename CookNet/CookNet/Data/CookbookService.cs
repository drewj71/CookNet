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
            return await _context.UserCookbooks.ToListAsync();
        }

        public async Task<UserCookbook?> GetCookbookByIdAsync(int cookbookID)
        {
            return await _context.UserCookbooks.FindAsync(cookbookID);
        }

        public async Task<List<UserCookbook>> GetCookbooksByUserIdAsync(string userId)
        {
            return await _context.UserCookbooks
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

        public async Task<List<UserCookbook>> GetCookbooksContainingRecipeAsync(int recipeId)
        {
            return await _context.UserCookbooks
                                   .Where(c => c.CookbookRecipes.Any(cr => cr.RecipeID == recipeId))
                                   .ToListAsync();
        }

        public async Task<List<Recipe>> GetRecipesByCookbookIdAsync(int cookbookId)
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

        public async Task CreateCookbookAsync(UserCookbook book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.UserCookbooks.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCookbookAsync(UserCookbook book)
        {
            var existingBook = await _context.UserCookbooks.FindAsync(book.CookbookID);
            if (existingBook != null)
            {
                _context.Entry(existingBook).CurrentValues.SetValues(book);
                await _context.SaveChangesAsync();
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
                if (book != null)
                {
                    _context.UserCookbooks.Remove(book);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Cookbook with ID {cookbookID} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"No cookbook found with ID {cookbookID}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting cookbook with ID {cookbookID}: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveCookbookRecipes(IEnumerable<CookbookRecipe> recipes)
        {
            if (recipes == null || !recipes.Any())
            {
                return;
            }

            _context.CookbookRecipes.RemoveRange(recipes);
            await _context.SaveChangesAsync();
        }

        public async Task AddRecipeToCookbooks(int recipeId, List<int> cookbookIds)
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

        public async Task RemoveRecipeFromCookbooks(int recipeId, List<int> cookbookIds)
        {
            var cookbookRecipesToRemove = await _context.CookbookRecipes
                .Where(cr => cookbookIds.Contains(cr.CookbookID) && cr.RecipeID == recipeId)
                .ToListAsync();

            _context.CookbookRecipes.RemoveRange(cookbookRecipesToRemove);
            await _context.SaveChangesAsync();
        }

        private readonly HashSet<string> AllowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

        private bool IsValidImage(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return AllowedExtensions.Contains(extension);
        }

        public async Task<string> UploadImageAsync(IBrowserFile file)
        {
            if (file == null || file.Size == 0 || string.IsNullOrEmpty(file.Name) || !IsValidImage(file.Name) || file.Size > MaxFileSize)
            {
                throw new ArgumentException("Invalid file or unsupported file type");
            }

            // Save the image and return its path
            return await SaveImageAsync(file);
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
                    await file.OpenReadStream().CopyToAsync(fileStream);
                }

                return Path.Combine("uploads", uniqueFileName);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                throw new InvalidOperationException("Error saving image file", ex);
            }
        }

    }
}
