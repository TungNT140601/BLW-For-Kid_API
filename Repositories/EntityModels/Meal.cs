using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Meal
    {
        public Meal()
        {
            Recipes = new HashSet<Recipe>();
        }

        public string MealId { get; set; } = null!;
        public string? MealName { get; set; }
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
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
