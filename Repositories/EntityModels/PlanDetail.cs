using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class PlanDetail
    {
        public string PlanDetailId { get; set; } = null!;
        public string PlanId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public int? Date { get; set; }
        public int? MealOfDate { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Plan Plan { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
