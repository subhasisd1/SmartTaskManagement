namespace SmartTaskManagement.Application.Interfaces;

using SmartTaskManagement.Application.DTOs.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto dto);

    Task<UserDto> RegisterAsync(RegisterDto dto);
}
