using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Recipe
    {
        public Recipe()
        {
            Directions = new HashSet<Direction>();
            Favorites = new HashSet<Favorite>();
            IngredientOfRecipes = new HashSet<IngredientOfRecipe>();
            PlanDetails = new HashSet<PlanDetail>();
            Ratings = new HashSet<Rating>();
        }

        public string RecipeId { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
        public string? StaffCreate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? StaffUpdate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? StaffDelete { get; set; }
        public string? MealId { get; set; }
        public string? RecipeImage { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }
        public string? AgeId { get; set; }
        public bool? ForPremium { get; set; }
        public bool? IsDelete { get; set; }
        public int? PrepareTime { get; set; }
        public int? StandTime { get; set; }
        public int? CookTime { get; set; }
        public int? TotalTime { get; set; }
        public int? Servings { get; set; }
        public string? RecipeName { get; set; }
        public string? RecipeDesc { get; set; }

        public virtual Age? Age { get; set; }
        public virtual Meal? Meal { get; set; }
        public virtual StaffAccount? StaffCreateNavigation { get; set; }
        public virtual StaffAccount? StaffDeleteNavigation { get; set; }
        public virtual StaffAccount? StaffUpdateNavigation { get; set; }
        public virtual ICollection<Direction>? Directions { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
        public virtual ICollection<IngredientOfRecipe>? IngredientOfRecipes { get; set; }
        public virtual ICollection<PlanDetail>? PlanDetails { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
    }
}
