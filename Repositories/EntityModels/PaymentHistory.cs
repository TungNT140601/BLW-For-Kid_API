using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class PaymentHistory
    {
        public string PaymentId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string PackageId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PayTime { get; set; }
        public bool PaymentStatus { get; set; }
        public string PrivateCode { get; set; } = null!;

        public virtual CustomerAccount? Customer { get; set; } = null!;
        public virtual PremiumPackage? Package { get; set; } = null!;
    }
}
