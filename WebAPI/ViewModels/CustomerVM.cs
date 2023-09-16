namespace WebAPI.ViewModels
{
    public class CustomerVM
    {
        public string CustomerId { get; set; } = null!;
        public string? Email { get; set; }
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public string? Fullname { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? PhoneNum { get; set; }
        public bool? IsPremium { get; set; }
        public bool? IsTried { get; set; }
        public bool? WasTried { get; set; }
    }
    public class CusLoginEmail
    {
        public string Email { get; set; }
        public string GoogleSub { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
    }
    public class CusLoginPhone
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class CusLoginFacebook
    {
        public string FacebookId { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
    }
    public class CusRegisPhone
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ResetPassword
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
    }
}
