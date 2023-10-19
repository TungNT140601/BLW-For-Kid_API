using System.Text.Json.Serialization;

namespace WebAPI.ViewModels
{
    public class IngredientVM
    {
        public string? IngredientId { get; set; }
        public string? IngredientName { get; set; }
        public string? Measure { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }

    }

    public class IngredientUpdateVM
    {
        public string? IngredientId { get; set; } = null!;
        public string? IngredientName { get; set; }
        public string? Measure { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }
        [JsonIgnore]
        public string? StaffUpdate { get; set; }
    }
    public class IngredientDeleteVM
    {
        public string? IngredientId { get; set; }
        [JsonIgnore]
        public string? StaffDelete { get; set; }
    }


}
