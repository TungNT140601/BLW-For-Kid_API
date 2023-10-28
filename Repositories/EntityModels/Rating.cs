using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Rating
    {
        public string RatingId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public int? Rate { get; set; }
        public DateTime? Date { get; set; }
        public string? RatingImage { get; set; }
        public bool? IsDelete { get; set; }
        public string? Comment { get; set; }

        public virtual CustomerAccount? Customer { get; set; } = null!;
        public virtual Recipe? Recipe { get; set; } = null!;
    }
}
