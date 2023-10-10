using System.Text.Json.Serialization;

namespace WebAPI.ViewModels
{
    public class RatingVM
    {
        [JsonIgnore]
        public string CustomerId { get; set; } = null!;
        [JsonIgnore]
        public string? Fullname { get; set; }
        [JsonIgnore]
        public string? Avatar { get; set; }
        public string RecipeId { get; set; } = null!;
        public int? Rate { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public DateTime? Date { get; set; }
        public string? RatingImage { get; set; }
    }
}
