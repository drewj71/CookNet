﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CookNet.Data
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int CookTime { get; set; }
        public string Ethnicity { get; set; }
        public string Category { get; set; }
        public string AuthorID { get; set; }
        public int PrepTime { get; set; }
        public ApplicationUser Author { get; set; }
        public string? ThumbnailImage { get; set; }
        public List<string>? AdditionalImages { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
        public ICollection<RecipeStory> RecipeStories { get; set; }

        public Recipe()
        {
            RecipeIngredients = new List<RecipeIngredient>();
            Instructions = new List<Instruction>();
            RecipeStories = new List<RecipeStory>();
            AdditionalImages = new List<string>();
        }
    }
}