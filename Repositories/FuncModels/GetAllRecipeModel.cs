using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FuncModels
{
    public class GetAllRecipeModel
    {
        public string? RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? MealId { get; set; }
        public string? MealName { get; set; }
        public string? RecipeImage { get; set; }
        public string? AgeId { get; set; }
        public string? AgeName { get; set; }
        public bool? IsFavorite { get; set; }
        public int? TotalFavorite { get; set; }
        public int? TotalRate { get; set; }
        public double? AveRate { get; set; }
        public bool? ForPremium { get; set; }
    }
}
