using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartTaskManagement.Application.DTOs.Auth;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SmartTaskManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        // 1. Find user
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        // 2. Validate password
        var passwordValid =
            await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        // 3. Create JWT claims
        var claims = new List<Claim>
        {
            // This is the most important claim for your user ID
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id),

            new Claim(
                ClaimTypes.Name,
                user.UserName ?? string.Empty),

            new Claim(
                ClaimTypes.Email,
                user.Email ?? string.Empty)
        };

        // 4. Get JWT configuration
        var jwtKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "JWT Key is not configured.");
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        // 5. Create security key
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        // 6. Create signing credentials
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // 7. Token expiration
        var expiration = DateTime.UtcNow.AddMinutes(15);

        // 8. Create JWT
        var jwtToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        // 9. Convert JWT object to string
        var token = new JwtSecurityTokenHandler()
            .WriteToken(jwtToken);

        // 10. Return response
        return new LoginResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            TokenExpiration = expiration,

            Data = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty
            }
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(
            user,
            dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description)));
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}