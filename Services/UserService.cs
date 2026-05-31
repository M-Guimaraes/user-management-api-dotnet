using AutoMapper;

using UserManagementApi.DTOs;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        
        return mapper.Map<IEnumerable<User>>(users);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await userRepository.GetByIdAsync(id);

        return user == null ? null : mapper.Map<User>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        User? user = await userRepository.GetByIdAsync(id);

        if (user == null) return false;
        
        await userRepository.DeleteAsync(user);
        
        return true;

    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        User? user = await userRepository.GetByIdAsync(id);
        
        if (user == null) return false;

        mapper.Map(dto, user);
 
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.SaveChangesAsync();

        return true;
    }
}
