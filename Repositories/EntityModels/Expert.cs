using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Expert
    {
        public Expert()
        {
            Chats = new HashSet<Chat>();
        }

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
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string? Description { get; set; }
        public string? ProfessionalQualification { get; set; }
        public string? WorkProgress { get; set; }
        public string? Achievements { get; set; }

        public virtual ICollection<Chat>? Chats { get; set; }
    }
}
