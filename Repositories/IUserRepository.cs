using UserManagementApi.Common;
using UserManagementApi.DTOs;

namespace UserManagementApi.Repositories;

public interface IUserRepository
{
    Task<PagedResult<User>> GetAllAsync(UserQueryDto query, CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);

    void Update(User user);

    void Delete(User user);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
