using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class IngredientOfRecipe
    {
        public string IngredientId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public double? Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
