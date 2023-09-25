namespace WebAPI.ViewModels
{
    public class AgeVM
    {
        public string? AgeName { get; set; }
    }

    public class AgeUpdateVM
    {
        public string AgeId { get; set; } = null!;
        public string? AgeName { get; set; }
    }
}
