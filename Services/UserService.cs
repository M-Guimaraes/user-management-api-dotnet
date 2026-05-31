using UserManagementApi.DTOs;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class UserService(IUserRepository userRepository) : IUserService
{

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
        
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return  await userRepository.GetByIdAsync(id);
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
        if (user == null)
            return false;

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.SaveChangesAsync();

        return true;
    }
}
