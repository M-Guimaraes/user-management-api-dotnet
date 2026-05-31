using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;

namespace UserManagementApi.Repositories;

public class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken token)
    {
        await context.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken?> GetByUserTokenAsync(string token)
    {
        return await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
}
