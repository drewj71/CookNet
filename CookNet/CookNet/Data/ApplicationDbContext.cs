using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CookNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<RecipeStory> RecipeStories { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.ID)
                .ValueGeneratedOnAdd();

            // Configure one-to-many relationship between Recipe and Instruction
            modelBuilder.Entity<Instruction>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Instructions)
                .HasForeignKey(i => i.RecipeID);

            // Configure one-to-many relationship between Recipe and RecipeStory
            modelBuilder.Entity<RecipeStory>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeStories)
                .HasForeignKey(rs => rs.RecipeID);

            // Configure many-to-many relationship between Recipe and Ingredient through RecipeIngredient
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeID, ri.IngredientID });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientID);

            // Modify the Ingredient entity to include the Quantity property
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Quantity)
                .IsRequired();
        }
    }
}
