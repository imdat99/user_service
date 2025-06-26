using Account.Application.DTOs;
using Account.Domain.Entities;

namespace Account.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.AccountStatus.ToString()));
            
        // Add more mappings here as needed
    }
}
