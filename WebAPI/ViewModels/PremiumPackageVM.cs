namespace WebAPI.ViewModels
{
    public class PremiumPackageVM
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
