using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class Chat
    {
        public Chat()
        {
            ChatHistories = new HashSet<ChatHistory>();
        }

        public string ChatId { get; set; } = null!;
        public string ExpertId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public DateTime? StartChat { get; set; }
        public bool? IsDelete { get; set; }

        public virtual CustomerAccount Customer { get; set; } = null!;
        public virtual Expert Expert { get; set; } = null!;
        public virtual ICollection<ChatHistory> ChatHistories { get; set; }
    }
}
