using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseChat.Application.DTOs
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

}
