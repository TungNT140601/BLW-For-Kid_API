using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Favorite
    {
        public string CustomerId { get; set; } = null!;
        public string RecipeId { get; set; } = null!;
        public DateTime FavoriteTime { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
