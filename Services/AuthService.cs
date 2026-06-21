using System.Security.Cryptography;
using System.Text;
using AutoMapper;

using UserManagementApi.DTOs;
using UserManagementApi.Exceptions;
using UserManagementApi.Exceptions.Http;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class AuthService(
    IUserRepository userRepository, 
    IRefreshTokenRepository refreshTokenRepository, 
    IMapper mapper,
    IJwtService jwtService) : IAuthService
{
    private const int RefreshTokenExpirationDays = 7;

    public async Task Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        
        User? registered = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (registered is not null) {
            throw new ConflictException("Email already registered");
        }
        
        string? passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
        };
        
        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        User user = await AuthenticateAsync(dto, cancellationToken);
        
        string token = jwtService.GenerateToken(user);
        string refreshToken = await CreateRefreshTokenAsync(user.Id, cancellationToken);
        
        return CreateAuthResponse(user, token, refreshToken);    
    }

    public async Task<AuthResponseDto?> RefreshToken(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken, cancellationToken);

        if (stored is null) {
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (stored.Revoked) {
            throw new UnauthorizedException("Refresh token has been revoked");
        }

        if (stored.ExpiresAt < DateTime.UtcNow) {
            throw new UnauthorizedException("Refresh token has expired");
        }

        // rotate refresh token: revoke the old one and create a new one
        stored.Revoked = true;
        
        string token = jwtService.GenerateToken(stored.User);
        string refreshToken = await CreateRefreshTokenAsync(stored.User.Id, cancellationToken);

        return CreateAuthResponse(stored.User, token, refreshToken);
    }

    public async Task Logout(RefreshTokenDto dto, CancellationToken cancellationToken)
    {
        RefreshToken? stored = await refreshTokenRepository.GetByUserTokenAsync(dto.RefreshToken, cancellationToken);

        if (stored is null) {
            throw new UnauthorizedException("Invalid refresh token");       
        }
        
        stored.Revoked = true;
        
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
    }
    
    private static string ComputeHash(string token)
    {
        using var sha = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(token);
        byte[] hashed = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashed);
    }

    private static string GenerateSecureToken(int size = 64)
    {
        byte[] data = new byte[size];
        RandomNumberGenerator.Fill(data);
        return Convert.ToBase64String(data).TrimEnd('=');
    }

    private async Task<string> CreateRefreshTokenAsync(int userId, CancellationToken cancellationToken)
    {
        string refreshToken = GenerateSecureToken();
        
        await refreshTokenRepository.AddAsync(
            new RefreshToken
            {
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays),
                TokenHash = ComputeHash(refreshToken),
            },
            cancellationToken
        );

        await refreshTokenRepository.SaveChangesAsync(cancellationToken);
        
        return refreshToken;       
    }

    private async Task<User> AuthenticateAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) {
            throw new UnauthorizedException("Invalid email or password");
        }
        
        return user;      
    }

    private AuthResponseDto CreateAuthResponse(User user, string jwt, string refreshToken)
    {
        var response = mapper.Map<AuthResponseDto>(user);
        
        response.Token = jwt;
        response.RefreshToken = refreshToken;
        
        return response;      
    }
}
