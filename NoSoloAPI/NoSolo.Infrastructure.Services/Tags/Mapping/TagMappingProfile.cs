using AutoMapper;
using NoSolo.Contracts.Dtos.Users.Tags;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Tags.Mapping;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<NewUserTagDto, UserTag>();
        CreateMap<UserTag, UserTagDto>();
        CreateMap<UserTagDto, UserTag>();
    }
}