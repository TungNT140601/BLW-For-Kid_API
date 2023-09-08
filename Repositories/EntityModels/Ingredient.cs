using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientOfRecipes = new HashSet<IngredientOfRecipe>();
        }

        public string IngredientId { get; set; } = null!;
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

        public virtual StaffAccount? StaffCreateNavigation { get; set; }
        public virtual StaffAccount? StaffDeleteNavigation { get; set; }
        public virtual StaffAccount? StaffUpdateNavigation { get; set; }
        public virtual ICollection<IngredientOfRecipe> IngredientOfRecipes { get; set; }
    }
}
