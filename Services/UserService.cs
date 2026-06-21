using AutoMapper;

using UserManagementApi.Common;
using UserManagementApi.DTOs;
using UserManagementApi.Exceptions.Http;
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
        User? user = await GetUserOrThrowAsync(id, cancellationToken);
        
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        User? user = await GetUserOrThrowAsync(id, cancellationToken); 
        
        userRepository.Delete(user);

        await userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        User? user = await GetUserOrThrowAsync(id, cancellationToken);
        
        mapper.Map(dto, user);
 
        await userRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<User> GetUserOrThrowAsync(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null) {
            throw new NotFoundException($"User with id {id} not found");
        }

        return user;
    }
}
