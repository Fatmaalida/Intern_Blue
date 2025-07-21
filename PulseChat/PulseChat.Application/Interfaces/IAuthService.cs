using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PulseChat.Application.DTOs;

namespace PulseChat.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginDto);
    Task<AuthResponseDto> RefreshAsync(string refreshToken);

}
