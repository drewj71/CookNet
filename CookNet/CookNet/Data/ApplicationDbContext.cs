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
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeImage> RecipeImages { get; set; }
        public DbSet<UserCookbook> UserCookbooks { get; set; }
        public DbSet<CookbookRecipe> CookbookRecipes { get; set; }
        public DbSet<RecipeRating> RecipeRatings { get; set; }
        public DbSet<RecipeComment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeImages)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeID);

            // Configure one-to-many relationship between Recipe and Instruction
            modelBuilder.Entity<Instruction>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Instructions)
                .HasForeignKey(i => i.RecipeID);

            // Configure many-to-many relationship between Recipe and Ingredient through RecipeIngredient
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeID, ri.IngredientID });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Recipe to RecipeIngredient

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientID)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete from RecipeIngredient to Ingredient

            // Modify the Ingredient entity to include the Quantity property
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Quantity)
                .IsRequired();

            modelBuilder.Entity<UserCookbook>()
                .HasMany(uc => uc.CookbookRecipes)
                .WithOne(cr => cr.UserCookbook)
                .HasForeignKey(cr => cr.CookbookID);

            modelBuilder.Entity<CookbookRecipe>()
                .HasOne(cr => cr.Recipe)
                .WithMany(r => r.CookbookRecipes) // This now works because Recipe has CookbookRecipes property
                .HasForeignKey(cr => cr.RecipeID);

            modelBuilder.Entity<UserCookbook>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCookbooks)
                .HasForeignKey(uc => uc.UserID);

            modelBuilder.Entity<RecipeRating>()
                .HasKey(rr => rr.RatingID);

            modelBuilder.Entity<RecipeRating>()
                .HasOne(rr => rr.User)
                .WithMany(u => u.RecipeRatings)
                .HasForeignKey(rr => rr.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeRating>()
                .HasOne(rr => rr.Recipe)
                .WithMany(r => r.RecipeRatings)
                .HasForeignKey(rr => rr.RecipeID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeComment>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(rc => rc.RecipeID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecipeComment>()
                .HasOne(rc => rc.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(rc => rc.UserID);

            modelBuilder.Entity<RecipeComment>()
                .HasOne(rc => rc.ParentComment)
                .WithMany(pc => pc.Replies)
                .HasForeignKey(rc => rc.ParentCommentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
