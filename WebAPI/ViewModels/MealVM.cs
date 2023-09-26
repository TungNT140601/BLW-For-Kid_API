namespace WebAPI.ViewModels
{
    public class MealAddVM
    {
        public string? MealName { get; set; }
        public string? StaffCreate { get; set; }
    }

    public class MealUpdateVM
    {
        public string MealId { get; set; } = null!;
        public string? MealName { get; set; }
        public string? StaffUpdate { get; set; }
    }

    public class MealDeleteVM
    {
        public string MealId { get; set; } = null!;
        public string? StaffDelete { get; set; }
    }
}
