using Repositories.EntityModels;
using System.Text.Json.Serialization;

namespace WebAPI.ViewModels
{
    public class PremiumPackageVM
    {
        public string PackageId { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public decimal PackageAmount { get; set; }
        public double? PackageDiscount { get; set; }
        public int PackageMonth { get; set; }
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }
    }
    public class PremiumPackageAddVM
    {
        public string PackageName { get; set; }
        public decimal PackageAmount { get; set; }
        public double? PackageDiscount { get; set; }
        public int PackageMonth { get; set; }
    }

    public class PremiumPackageUpdateVM
    {
        public string PackageId { get; set; } = null!;
        public string PackageName { get; set; }
        public decimal PackageAmount { get; set; }
        public double? PackageDiscount { get; set; }
        public int PackageMonth { get; set; }
    }
    
}
