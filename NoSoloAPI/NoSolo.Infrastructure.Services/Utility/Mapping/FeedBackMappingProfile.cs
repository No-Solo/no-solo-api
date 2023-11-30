using AutoMapper;
using NoSolo.Contracts.Dtos.FeedBack;
using NoSolo.Core.Entities.FeedBack;

namespace NoSolo.Infrastructure.Services.Utility.Mapping;

public class FeedBackMappingProfile : Profile
{
    public FeedBackMappingProfile()
    {
        CreateMap<NewFeedBackDto, FeedBack>();
        CreateMap<FeedBack, FeedBackDto>();
    }
}