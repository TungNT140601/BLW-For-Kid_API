namespace WebAPI.ViewModels
{
    public class PlanVM
    {
        public string PlanId { get; set; }
        public string? PlanName { get; set; }
        public string? AgeName { get; set; }
    }
    public class PlanAddVM
    {
        public string PlanId { get; set; }
        public string? PlanName { get; set; }
        public string? AgeId { get; set; }
        public IEnumerable<PlanDetailAddVM> PlanDetails { get; set; }
    }
    public class PlanDetailAddVM
    {
        public string RecipeId { get; set; }
        public int? Date { get; set; }
        public int? MealOfDate { get; set; }
    }
    public class PlanDetailVM
    {
        public string PlanDetailId { get; set; }
        public RecipeVM RecipeVM { get; set; }
    }
}
