using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public class UserService(AppDbContext context) : IUserService
{

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.AsNoTracking().ToListAsync();
        
    }

    public User Create (CreateUserDto dto)
    {
        var newUser = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        context.Users.Add(newUser);
        context.SaveChanges();
        
        return newUser;
    }

    public User? GetById(int id)
    {
        return context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
    }
    
    public bool Delete(int id)
    {
        User? user = context.Users.FirstOrDefault(u => u.Id == id);
        
        if (user != null)
        {
            context.Remove(user);
            context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool  Update(int id, UpdateUserDto dto)
    {
        User? user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return false;
        
        user.Name = dto.Name;
        user.Email = dto.Email;
        user.UpdatedAt = DateTime.UtcNow;
        
        context.SaveChanges();

        return true;
    }
}
