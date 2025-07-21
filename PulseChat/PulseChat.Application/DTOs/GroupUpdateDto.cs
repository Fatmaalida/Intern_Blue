using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace PulseChat.Application.DTOs;

public class GroupUpdateDto
{
    [Required]
    public string Name { get; set; } = null!;

    public bool IsPrivate { get; set; } = false;
}
