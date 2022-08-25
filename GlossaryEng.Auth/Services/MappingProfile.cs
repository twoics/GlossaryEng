using AutoMapper;
using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;

namespace GlossaryEng.Auth.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, UserDb>()
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