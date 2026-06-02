using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

using UserManagementApi.Data;

namespace UserManagementApi.Repositories;

public class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
    {
        await context.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public async Task<RefreshToken?> GetByUserTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        string hash = ComputeHash(token);

        return await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.TokenHash == hash, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    private static string ComputeHash(string token)
    {
        using var sha = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(token);
        byte[] hashed = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashed);
    }
    
}
