using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Direction
    {
        public string DirectionId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public int? DirectionNum { get; set; }
        public string? DirectionDesc { get; set; }
        public string? DirectionImage { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
