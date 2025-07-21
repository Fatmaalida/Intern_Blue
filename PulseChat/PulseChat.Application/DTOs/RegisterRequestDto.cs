﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;


namespace PulseChat.Application.DTOs;

public class RegisterRequestDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

