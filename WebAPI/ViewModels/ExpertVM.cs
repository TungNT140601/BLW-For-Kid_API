namespace WebAPI.ViewModels
{
    public class ExpertVM
    {
        public string ExpertId { get; set; } = null!;
        public string? Email { get; set; }
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public string? PhoneNum { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Position { get; set; }
        public string? WorkUnit { get; set; }
        public string? Description { get; set; }
        public string? ProfessionalQualification { get; set; }
        public string? WorkProgress { get; set; }
        public string? Achievements { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }

    public class ResetPasswordExpert
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }

    public class ExpLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

