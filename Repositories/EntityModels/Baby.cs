using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Baby
    {
        public Baby()
        {
            GrowHistories = new HashSet<GrowHistory>();
        }

        public string BabyId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string? Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Bmi { get; set; }
        public string? HealthType { get; set; }
        public bool? IsDelete { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
        public virtual ICollection<GrowHistory> GrowHistories { get; set; }
    }
}
