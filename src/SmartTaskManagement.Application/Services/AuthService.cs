using Microsoft.AspNetCore.Identity;
using SmartTaskManagement.Application.DTOs.Auth;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            return new LoginResponseDto { Success = false, Message = "Invalid credentials" };
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
        {
            return new LoginResponseDto { Success = false, Message = "Invalid credentials" };
        }

        // For now return a simple response without JWT generation (not configured in project)
        return new LoginResponseDto
        {
            Success = true,
            Message = "Login successful",
            Data = new UserDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName
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

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
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
