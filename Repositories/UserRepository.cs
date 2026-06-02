using Microsoft.EntityFrameworkCore;

using UserManagementApi.Common;
using UserManagementApi.Data;
using UserManagementApi.DTOs;

namespace UserManagementApi.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{

    public async Task<PagedResult<User>> GetAllAsync(UserQueryDto query, CancellationToken cancellationToken = default)
    {
        var usersQuery = context.Users.AsNoTracking();
        string? name = query.Name?.Trim();
        string? email = query.Email?.Trim();
        
        if (!string.IsNullOrWhiteSpace(name)) {
            usersQuery = usersQuery.Where(u => u.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(email)) {
            usersQuery = usersQuery.Where(u => u.Email.Equals(email));
        }
        
        int totalItems = await usersQuery.CountAsync(cancellationToken);
        
        var users = await usersQuery
            .OrderBy(u => u.Name)
            .Skip(query.Skip)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedResult<User>
        {
            Items = users,
            TotalItems = totalItems,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }

    public void Delete(User user)
    {
        user.DeletedAt = DateTime.UtcNow;
        user.IsDeleted = true;
        
        context.Users.Update(user);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
