using AutoMapper;
using NoSolo.Abstractions.Repositories.FeedBack;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.FeedBack;
using NoSolo.Core.Entities.FeedBack;

namespace NoSolo.Infrastructure.Services.Utility;

public class FeedBackService(IFeedBackRepository feedBackRepository, IMapper mapper) : IFeedBackService
{
    public async Task<FeedBackDto> Create(NewFeedBackDto newFeedBackDto)
    {
        var feedBack = new FeedBackEntity()
        {
            FirstName = newFeedBackDto.FirstName,
            LastName = newFeedBackDto.LastName,
            Email = newFeedBackDto.Email,
            FeedBackText = newFeedBackDto.FeedBackText
        };

        feedBackRepository.AddAsync(feedBack);
        feedBackRepository.Save();
        
        return mapper.Map<FeedBackDto>(feedBack);
    }

    public async Task<FeedBackDto> Get(Guid feedbackGuid)
    {
        return mapper.Map<FeedBackDto>(await feedBackRepository.Get(feedbackGuid));
    }

    public async Task<IReadOnlyList<FeedBackDto>> Get()
    {
        return mapper.Map<IReadOnlyList<FeedBackDto>>(await feedBackRepository.Get());
    }

    public async Task Delete(Guid feedbackGuid)
    {
        await feedBackRepository.Delete(await feedBackRepository.Get(feedbackGuid));
        feedBackRepository.Save();
    }
}