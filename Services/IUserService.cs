using UserManagementApi.Common;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IUserService
{
    Task<PagedResult<UserResponseDto>> GetAllAsync(UserQueryDto query, CancellationToken cancellationToken);
    
    Task<UserResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task DeleteAsync(int id, CancellationToken cancellationToken);

    Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken cancellationToken);
}
