namespace WebAPI.ViewModels
{
    public class DirectionVM
    {
        public string? DirectionId { get; set; }
        public string? RecipeId { get; set; }
        public int? DirectionNum { get; set; }
        public string? DirectionDesc { get; set; }
        public string? DirectionImage { get; set; }
    }
    public class DirectionAddVM
    {
        public string? DirectionId { get; set; }
        public int? DirectionNum { get; set; }
        public string? DirectionDesc { get; set; }
        public string? DirectionImage { get; set; }
    }
}
