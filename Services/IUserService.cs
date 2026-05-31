using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    
    Task<User?> GetByIdAsync(int id);
    
    Task<bool> DeleteAsync(int id);
    
    Task<bool> UpdateAsync(int id, UpdateUserDto dto);
}
