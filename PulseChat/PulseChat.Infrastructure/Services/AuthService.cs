using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;
using PulseChat.Domain.Entities;

namespace PulseChat.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerDto)
    {
        if (await _userRepository.IsEmailTakenAsync(registerDto.Email))
            throw new Exception("Email already in use");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            PasswordHash = hashedPassword
        };

        await _userRepository.AddAsync(user);
        return await GenerateTokensAsync(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return await GenerateTokensAsync(user);
    }

    private async Task<AuthResponseDto> GenerateTokensAsync(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var expires = DateTime.UtcNow.AddMinutes(30);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        // 🔄 Refresh token oluştur ve kaydet
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        await _refreshTokenRepository.AddAsync(refreshToken);

        return new AuthResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken.Token,
            ExpiresAt = expires
        };
    }

    public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (token == null || token.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Invalid or expired refresh token");

        var user = await _userRepository.GetByIdAsync(token.UserId);
        if (user == null)
            throw new Exception("User not found");

        // Eski refresh token'ı sil (opsiyonel ama tavsiye edilir)
        //await _refreshTokenRepository.DeleteAsync(token.Id);

        return await GenerateTokensAsync(user);
    }
}
