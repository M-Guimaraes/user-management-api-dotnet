using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public class AuthService(AppDbContext context, IJwtService jwtService) : IAuthService
{

    public async Task<bool> Register(RegisterDto dto)
    {
        
        User? registered = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (registered != null) {
            return false;
        }
        
        string? passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        await context.Users.AddAsync(new User {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash,
        });
        
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        
        if (user == null) return null;

        var passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        
        var token = jwtService.GenerateToken(user);
        
        if (!passwordMatch) return null;

        var authUser = new AuthResponseDto {
            Email = user.Email,
            Name = user.Name,
            Token = token,
        };
        
        return authUser;       
    }
}
