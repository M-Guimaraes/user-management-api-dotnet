using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IAuthService
{
    Task Register(RegisterDto dto, CancellationToken cancellationToken);
    
    Task<AuthResponseDto?> Login(LoginDto dto, CancellationToken cancellationToken);

    Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken);
    
    Task Logout(RefreshTokenDto dto, CancellationToken cancellationToken);
}
