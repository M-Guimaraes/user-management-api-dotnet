using AutoMapper;

using UserManagementApi.DTOs;

namespace UserManagementApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        
        CreateMap<CreateUserDto, User>();
        
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<User, AuthResponseDto>();
    }
}