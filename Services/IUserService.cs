using UserManagementApi.DTOs;

namespace UserManagementApi.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    
    User? GetById(int id);
    
    bool Delete(int id);
    
    bool Update(int id, UpdateUserDto dto);
}
