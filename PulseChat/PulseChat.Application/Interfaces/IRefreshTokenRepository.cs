using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;


using PulseChat.Domain.Entities;

namespace PulseChat.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task InvalidateAsync(RefreshToken token);
}
