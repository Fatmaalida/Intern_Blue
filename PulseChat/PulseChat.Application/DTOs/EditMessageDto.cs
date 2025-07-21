using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EditMessageDto
{
    public Guid SenderId { get; set; }
    public string NewContent { get; set; } = string.Empty;
}
