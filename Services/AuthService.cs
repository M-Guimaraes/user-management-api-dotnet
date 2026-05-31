using UserManagementApi.DTOs;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class AuthService(
    IUserRepository userRepository, 
    IRefreshTokenRepository refreshTokenRepository, 
    IJwtService jwtService) : IAuthService
{

    public async Task<bool> Register(RegisterDto dto)
    {
        
        User? registered = await userRepository.GetByEmailAsync(dto.Email);

        if (registered != null) {
            return false;
        }
        
        string? passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
        };
        
        await userRepository.AddAsync(user);
        
        await userRepository.SaveChangesAsync();

        return true;
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto)
    {
        User? user = await userRepository.GetByEmailAsync(dto.Email);
        
        if (user == null) return null;

        bool passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        
        if (!passwordMatch) return null;
        
        string token = jwtService.GenerateToken(user);
        
        string refreshToken = Guid.NewGuid().ToString();
        
        await refreshTokenRepository.AddAsync(new RefreshToken {
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Token = refreshToken,
        });
        
        await refreshTokenRepository.SaveChangesAsync();
        
        var authUser = new AuthResponseDto {
            Email = user.Email,
            Name = user.Name,
            Token = token,
            RefreshToken = refreshToken,
        };
        
        return authUser;       
    }

    public async Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken);

        if (stored == null) return null;
        
        if (stored.Revoked) return null;
        
        if (stored.ExpiresAt < DateTime.UtcNow) return null;       
        
        return new AuthResponseDto {
            Email = stored.User.Email,
            Name = stored.User.Name,
            Token = jwtService.GenerateToken(stored.User),
            RefreshToken = dto.RefreshToken,
        };
    }

    public async Task<bool> Logout(RefreshTokenDto dto)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken);
        
        if (stored == null) return false;
        
        stored.Revoked = true;
        
        await refreshTokenRepository.SaveChangesAsync();
        
        return true;       
    }


}
