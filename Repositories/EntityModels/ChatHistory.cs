using System;
using System.Collections.Generic;

namespace Repositories.EntityModels
{
    public partial class ChatHistory
    {
        public string ChatHistoryId { get; set; } = null!;
        public string ChatId { get; set; } = null!;
        public DateTime SendTime { get; set; }
        public string SendPerson { get; set; } = null!;
        public string? Image { get; set; }
        public string? Message { get; set; }
        public bool IsRemove { get; set; }

        public virtual Chat? Chat { get; set; } = null!;
    }
}
