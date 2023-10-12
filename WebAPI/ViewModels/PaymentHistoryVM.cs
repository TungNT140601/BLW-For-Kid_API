using Repositories.EntityModels;

namespace WebAPI.ViewModels
{
    public class PaymentHistoryVM
    {
        public string? PaymentId { get; set; }
        public string? CustomerId { get; set; }
        public string? PackageId { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PayTime { get; set; }
        public bool? PaymentStatus { get; set; }
        public string? PrivateCode { get; set; }

        public virtual CustomerVM? Customer { get; set; }
        public virtual PremiumPackageVM? Package { get; set; }
    }
    public class PaymentHistoryAllVM
    {
        public string? PaymentId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? PackageId { get; set; }
        public string? PackageName { get; set; }
        public decimal? PackagePrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PayTime { get; set; }
        public bool? PaymentStatus { get; set; }
        public string? PrivateCode { get; set; }
    }
}
