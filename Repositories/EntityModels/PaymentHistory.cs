using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class PaymentHistory
    {
        public string PaymentId { get; set; } = null!; //vnp_TxnRef
        public string CustomerId { get; set; } = null!;
        public double Amount { get; set; }
        public string CreateDateS { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string IpAddr { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string TxnRef { get; set; } = null!;
        public string ResponseCode { get; set; } = null;
        public string TransactionNo { get; set; } = null;
        public DateTime? PurchaseTime { get; set; }
        public int? NumOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
    }
}
