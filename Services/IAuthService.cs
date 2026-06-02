using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IAuthService
{
    Task<bool> Register(RegisterDto dto, CancellationToken cancellationToken);
    
    Task<AuthResponseDto?> Login(LoginDto dto, CancellationToken cancellationToken);

    Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken);
    
    Task<bool> Logout(RefreshTokenDto dto, CancellationToken cancellationToken);
}
