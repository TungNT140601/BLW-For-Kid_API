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
        public string PaymentChannel { get; set; } = null!;
        public string? MomoRequestId { get; set; }
        public string? MomoOrderId { get; set; }
        public string? MomoOrderInfo { get; set; }
        public string? MomoResponseMsg { get; set; }
        public string? MomoResultCode { get; set; }
        public string? MomoPayType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
        public virtual PremiumPackage Package { get; set; } = null!;
    }
}
