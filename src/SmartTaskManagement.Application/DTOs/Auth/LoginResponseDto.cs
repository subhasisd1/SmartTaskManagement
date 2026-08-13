namespace SmartTaskManagement.Application.DTOs.Auth;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public UserDto? Data { get; set; }
}
