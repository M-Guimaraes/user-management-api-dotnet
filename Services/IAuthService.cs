using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IAuthService
{
    Task<bool> Register(RegisterDto dto);
    
    Task<AuthResponseDto?> Login(LoginDto dto);

    Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto);
    
    Task<bool> Logout(RefreshTokenDto dto);
}
