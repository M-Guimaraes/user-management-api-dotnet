using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;

namespace UserManagementApi.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        context.Users.Update(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        context.Users.Remove(user);
        
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
