namespace UserManagementApi.Repositories;

public interface IRefreshTokenRepository
{

    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);

    Task<RefreshToken?> GetByUserTokenAsync(string token, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
