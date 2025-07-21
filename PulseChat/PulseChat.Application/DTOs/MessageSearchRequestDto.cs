﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseChat.Application.DTOs;

public class MessageSearchRequestDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Query { get; set; }  // Arama kelimesi (opsiyonel)
}
