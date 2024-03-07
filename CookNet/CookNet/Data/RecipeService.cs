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

    }
}
