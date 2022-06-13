using AutoMapper;
using Io.TedTalk.Core.DTOs;
using Io.TedTalk.Core.Entities;

namespace IO.TedTalk.Services;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<CreateTedDto, Ted>()
            .ForMember(x => x.Id, opt => opt.Ignore());
    }
}
