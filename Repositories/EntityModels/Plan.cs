using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Plan
    {
        public Plan()
        {
            PlanDetails = new HashSet<PlanDetail>();
        }

        public string PlanId { get; set; } = null!;
        public string? PlanName { get; set; }
        public string? AgeId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Age? Age { get; set; }
        public virtual ICollection<PlanDetail> PlanDetails { get; set; }
    }
}
