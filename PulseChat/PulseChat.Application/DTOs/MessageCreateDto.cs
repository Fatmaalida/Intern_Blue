using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseChat.Application.DTOs
{
    public class MessageCreateDto
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string Content { get; set; } = null!;

        public Guid SenderId { get; set; }

        public string? FileUrl { get; set; }
    }

}
