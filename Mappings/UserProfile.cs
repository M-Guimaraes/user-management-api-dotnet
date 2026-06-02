using AutoMapper;

using UserManagementApi.DTOs;

namespace UserManagementApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        
        CreateMap<User, CreateUserDto>();
        
        CreateMap<User, UpdateUserDto>()
            .ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<User, AuthResponseDto>();
    }
}