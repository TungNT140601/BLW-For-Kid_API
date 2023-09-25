namespace WebAPI.ViewModels
{
    public class IngredientOfRecipeVM
    {
        public string? IngredientId { get; set; }
        public string? RecipeId { get; set; }
        public double? Quantity { get; set; }
        public string? IngredientName { get; set; }
        public string? IngredientImage { get; set; }
        public string? Measure { get; set; }
    }
    public class IngredientOfRecipeAddVM
    {
        public string? IngredientId { get; set; }
        public string? RecipeId { get; set; }
        public double? Quantity { get; set; }
    }
}
