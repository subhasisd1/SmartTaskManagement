namespace SmartTaskManagement.Application.DTOs.Auth;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public DateTime TokenExpiration { get; set; }

    public UserDto? Data { get; set; }

}
