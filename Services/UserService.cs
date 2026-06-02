using AutoMapper;

using UserManagementApi.Common;
using UserManagementApi.DTOs;
using UserManagementApi.Repositories;

namespace UserManagementApi.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{

    public async Task<PagedResult<UserResponseDto>> GetAllAsync(UserQueryDto query, CancellationToken cancellationToken)
    {
        var pagedUsers = await userRepository.GetAllAsync(query, cancellationToken);

        return new PagedResult<UserResponseDto> {
            Items = mapper.Map<List<UserResponseDto>>(pagedUsers.Items),
            Page = pagedUsers.Page,
            PageSize = pagedUsers.PageSize,
            TotalItems = pagedUsers.TotalItems,
        };
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user == null) {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user == null) {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        
        userRepository.Delete(user);

        await userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user == null) {
            throw new KeyNotFoundException($"User with id {id} not found");       
        }
        
        if (dto.Name is not null)
        {
            user.Name = dto.Name;
        }

        if (dto.Email is not null)
        {
            user.Email = dto.Email;
        }

        mapper.Map(dto, user);
 
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.SaveChangesAsync(cancellationToken);
    }
}
