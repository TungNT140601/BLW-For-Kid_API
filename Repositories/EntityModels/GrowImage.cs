using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class GrowImage
    {
        public string ImageId { get; set; } = null!;
        public string? GrowId { get; set; }
        public string? ImageLink { get; set; }
        public bool? IsDelete { get; set; }

        public virtual GrowHistory? Grow { get; set; }
    }
}
