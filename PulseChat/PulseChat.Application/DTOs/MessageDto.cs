using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseChat.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public string UserName { get; set; } = null!;
        public string? FileUrl { get; set; }

    }
}
