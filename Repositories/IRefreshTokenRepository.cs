namespace UserManagementApi.Repositories;

public interface IRefreshTokenRepository
{

    Task AddAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByUserTokenAsync(string token);

    Task SaveChangesAsync();
}
