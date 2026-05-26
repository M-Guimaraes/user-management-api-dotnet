using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public class UserService : IUserService
{
    
    private readonly AppDbContext _context;
    
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
        
    }

    public User Create (CreateUserDto dto)
    {
        var newUser = new User
        {
            Name = dto.Name,
            Email = dto.Email
        };
        
        _context.Users.Add(newUser);
        _context.SaveChanges();
        
        return newUser;
    }

    public User? GetById(int id)
    {
        return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
    }
    
    public bool Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        
        if (user != null)
        {
            _context.Remove(user);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool  Update(int id, UpdateUserDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return false;
        
        user.Name = dto.Name;
        user.Email = dto.Email;
        
        _context.SaveChanges();

        return true;
    }
}
