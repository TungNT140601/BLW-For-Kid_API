using System.Text.Json.Serialization;

namespace WebAPI.ViewModels
{
    public class FavoriteVM
    {
        public string RecipeId { get; set; }
        [JsonIgnore]
        public DateTime FavoriteTime { get; set; }
    }
}
