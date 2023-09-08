using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Age
    {
        public Age()
        {
            Plans = new HashSet<Plan>();
            Recipes = new HashSet<Recipe>();
        }

        public string AgeId { get; set; } = null!;
        public string? AgeName { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
