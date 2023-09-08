using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class PaymentHistory
    {
        public string PaymentId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public DateTime PurchaseTime { get; set; }
        public int? NumOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
    }
}
