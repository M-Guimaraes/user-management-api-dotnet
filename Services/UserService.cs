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

    public async Task<User?> GetByIdAsync(int id)
    {
        return  await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return false;
        
        context.Remove(user);
        
        await context.SaveChangesAsync();
            
        return true;

    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return false;

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return true;
    }
}
