using AutoMapper;

using UserManagementApi.DTOs;

namespace UserManagementApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        
        CreateMap<CreateUserDto, User>();
        
        CreateMap<UpdateUserDto, User>();

        CreateMap<User, AuthResponseDto>();
    }
}