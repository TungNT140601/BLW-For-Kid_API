namespace WebAPI.ViewModels
{
    public class IngredientVM
    {
        public string? IngredientId { get; set; }
        public string? IngredientName { get; set; }
        public string? IngredientImage { get; set; }
        public string? Measure { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrate { get; set; }
        public double? Fat { get; set; }
        public double? Calories { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? StaffCreate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? StaffUpdate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? StaffDelete { get; set; }
        public bool? IsDelete { get; set; }

    }
}
