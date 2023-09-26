using Repositories.EntityModels;

namespace WebAPI.ViewModels
{
    public class RecipeVM
    {
        public string? RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? MealName { get; set; }
        public string? RecipeImage { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }
        public string? AgeName { get; set; }
        public int? TotalFavorite { get; set; }
        public int? TotalRate { get; set; }
        public double? AveRate { get; set; }
        public bool? ForPremium { get; set; }
        public IEnumerable<DirectionVM> DirectionVMs { get; set; }
        public IEnumerable<IngredientOfRecipeVM> IngredientOfRecipeVMs { get; set; }
        public IEnumerable<RatingVM> RatingVMs { get; set; }
    }
    public class RecipeAddUpdateVM
    {
        public string? RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? MealId { get; set; }
        public List<string>? RecipeImage { get; set; }
        public string? AgeId { get; set; }
        public bool? ForPremium { get; set; }
        public List<DirectionAddVM>? DirectionVMs { get; set; }
        public List<IngredientOfRecipeAddVM>? IngredientOfRecipeVMs { get; set; }
    }
}
