using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels
{
    public class StaffAccountVM
    {
        public string StaffId { get; set; } = null!;
        public string? Email { get; set; }
        public string? GoogleId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public int? Role { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }

    public class StaffAccountAddVM
    {
        public string? Email { get; set; }
        [Required(ErrorMessage = "This Field is Required!!!!")]
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class StaffAccountUpdateVM
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
