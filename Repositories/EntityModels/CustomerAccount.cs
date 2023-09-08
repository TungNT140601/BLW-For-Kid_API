using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class CustomerAccount
    {
        public CustomerAccount()
        {
            Babies = new HashSet<Baby>();
            Chats = new HashSet<Chat>();
            Favorites = new HashSet<Favorite>();
            PaymentHistories = new HashSet<PaymentHistory>();
            Ratings = new HashSet<Rating>();
        }

        public string CustomerId { get; set; } = null!;
        public string? Email { get; set; }
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public string? Fullname { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? PhoneNum { get; set; }
        public string? Password { get; set; }
        public bool? IsPremium { get; set; }
        public DateTime? SigupDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public bool? IsTried { get; set; }
        public int? NumOfTried { get; set; }
        public int? WasTried { get; set; }
        public DateTime? StartTriedDate { get; set; }
        public DateTime? EndTriedDate { get; set; }
        public int? NumOfPremiumMonths { get; set; }
        public DateTime? LastStartPremiumDate { get; set; }
        public DateTime? LastEndPremiumDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Baby> Babies { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
