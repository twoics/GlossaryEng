using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Request;

namespace GlossaryEng.Auth.Services;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<RegisterRequest, IdentityUser>()
            // Map User Name
            .ForMember(dest => dest.UserName,
                opt
                    => opt.MapFrom(src => src.Name))
            
            // Map Email
            .ForMember(dest => dest.Email,
                opt
                    => opt.MapFrom(src => src.Email));
    }
}