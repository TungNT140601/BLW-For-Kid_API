using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class GrowHistory
    {
        public GrowHistory()
        {
            GrowImages = new HashSet<GrowImage>();
        }

        public string GrowId { get; set; } = null!;
        public string BabyId { get; set; } = null!;
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Bmi { get; set; }
        public string? HealthType { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Baby Baby { get; set; } = null!;
        public virtual ICollection<GrowImage> GrowImages { get; set; }
    }
}
