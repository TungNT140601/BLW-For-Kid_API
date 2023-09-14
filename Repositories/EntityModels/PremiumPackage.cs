using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class PremiumPackage
    {
        public PremiumPackage()
        {
            PaymentHistories = new HashSet<PaymentHistory>();
        }

        public string PackageId { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public decimal PackageAmount { get; set; }
        public double? PackageDiscount { get; set; }
        public int PackageMonth { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }
    }
}
