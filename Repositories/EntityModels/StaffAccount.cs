using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class StaffAccount
    {
        public StaffAccount()
        {
            IngredientStaffCreateNavigations = new HashSet<Ingredient>();
            IngredientStaffDeleteNavigations = new HashSet<Ingredient>();
            IngredientStaffUpdateNavigations = new HashSet<Ingredient>();
            MealStaffCreateNavigations = new HashSet<Meal>();
            MealStaffDeleteNavigations = new HashSet<Meal>();
            MealStaffUpdateNavigations = new HashSet<Meal>();
            RecipeStaffCreateNavigations = new HashSet<Recipe>();
            RecipeStaffDeleteNavigations = new HashSet<Recipe>();
            RecipeStaffUpdateNavigations = new HashSet<Recipe>();
        }

        public string StaffId { get; set; } = null!;
        public string? Email { get; set; }
        public string? GoogleId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public int? Role { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Ingredient> IngredientStaffCreateNavigations { get; set; }
        public virtual ICollection<Ingredient> IngredientStaffDeleteNavigations { get; set; }
        public virtual ICollection<Ingredient> IngredientStaffUpdateNavigations { get; set; }
        public virtual ICollection<Meal> MealStaffCreateNavigations { get; set; }
        public virtual ICollection<Meal> MealStaffDeleteNavigations { get; set; }
        public virtual ICollection<Meal> MealStaffUpdateNavigations { get; set; }
        public virtual ICollection<Recipe> RecipeStaffCreateNavigations { get; set; }
        public virtual ICollection<Recipe> RecipeStaffDeleteNavigations { get; set; }
        public virtual ICollection<Recipe> RecipeStaffUpdateNavigations { get; set; }
    }
}
