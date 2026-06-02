using AutoMapper;

using UserManagementApi.DTOs;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class AuthService(
    IUserRepository userRepository, 
    IRefreshTokenRepository refreshTokenRepository, 
    IMapper mapper,
    IJwtService jwtService) : IAuthService
{

    public async Task<bool> Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        
        User? registered = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (registered != null) {
            return false;
        }
        
        string? passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
        };
        
        await userRepository.AddAsync(user, cancellationToken);
        
        await userRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);
        
        if (user == null) return null;

        bool passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        
        if (!passwordMatch) return null;
        
        string token = jwtService.GenerateToken(user);
        
        string refreshToken = Guid.NewGuid().ToString();

        var addUser = new RefreshToken {
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Token = refreshToken,
        };
        
        await refreshTokenRepository.AddAsync(addUser, cancellationToken);
        
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
        
        var authUser = mapper.Map<AuthResponseDto>(user);
        
        authUser.RefreshToken = refreshToken;
        authUser.Token = token;
        
        return authUser;       
    }

    public async Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken, cancellationToken);

        if (stored == null) return null;
        
        if (stored.Revoked) return null;
        
        if (stored.ExpiresAt < DateTime.UtcNow) return null;       

        var authUser = mapper.Map<AuthResponseDto>(stored.User);

        authUser.Token = jwtService.GenerateToken(stored.User);
        authUser.RefreshToken = dto.RefreshToken;
        
        return authUser;      
    }

    public async Task<bool> Logout(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken, cancellationToken);
        
        if (stored == null) return false;
        
        stored.Revoked = true;
        
        await refreshTokenRepository.SaveChangesAsync();
        
        return true;       
    }


}
