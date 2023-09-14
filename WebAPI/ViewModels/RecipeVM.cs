using Repositories.EntityModels;

namespace WebAPI.ViewModels
{
    public class RecipeVM
    {
        public string RecipeId { get; set; } = null!;
        public string? RecipeName { get; set; }
        public string? MealName { get; set; }
        public string? RecipeImage { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }
        public string? AgeName { get; set; }
    }
}
