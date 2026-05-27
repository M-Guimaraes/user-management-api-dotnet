using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public class AuthService : IAuthService
{

    private readonly AppDbContext _context;
    
    private readonly IJwtService _jwtService;
    
    public AuthService(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    
    public async Task<bool> Register(RegisterDto dto)
    {
        
        var registered = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (registered != null) {
            return false;
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        await _context.Users.AddAsync(new User {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash
        });
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        
        if (user == null) return null;

        var passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        
        var token = _jwtService.GenerateToken(user);
        
        if (!passwordMatch) return null;

        var authUser = new AuthResponseDto {
            Email = user.Email,
            Name = user.Name,
            Token = token,
        };
        
        return authUser;       
    }
}
