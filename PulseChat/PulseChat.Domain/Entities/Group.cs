using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;

namespace PulseChat.Domain.Entities;

public class Group
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public bool IsPrivate { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
