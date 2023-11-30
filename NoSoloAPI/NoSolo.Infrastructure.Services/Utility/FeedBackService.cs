using AutoMapper;
using NoSolo.Abstractions.Repositories.FeedBack;
using NoSolo.Abstractions.Services.Utility;
using NoSolo.Contracts.Dtos.FeedBack;
using NoSolo.Core.Entities.FeedBack;

namespace NoSolo.Infrastructure.Services.Utility;

public class FeedBackService : IFeedBackService
{
    private readonly IFeedBackRepository _feedBackRepository;
    private readonly IMapper _mapper;

    public FeedBackService(IFeedBackRepository feedBackRepository, IMapper mapper)
    {
        _feedBackRepository = feedBackRepository;
        _mapper = mapper;
    }

    public async Task<FeedBackDto> Create(NewFeedBackDto newFeedBackDto)
    {
        var feedBack = new FeedBack()
        {
            FirstName = newFeedBackDto.FirstName,
            LastName = newFeedBackDto.LastName,
            Email = newFeedBackDto.Email,
            FeedBackText = newFeedBackDto.FeedBackText
        };

        _feedBackRepository.AddAsync(feedBack);
        _feedBackRepository.Save();
        
        return _mapper.Map<FeedBackDto>(feedBack);
    }

    public async Task<FeedBackDto> Get(Guid feedbackGuid)
    {
        return _mapper.Map<FeedBackDto>(await _feedBackRepository.Get(feedbackGuid));
    }

    public async Task<IReadOnlyList<FeedBackDto>> Get()
    {
        return _mapper.Map<IReadOnlyList<FeedBackDto>>(await _feedBackRepository.Get());
    }

    public async Task Delete(Guid feedbackGuid)
    {
        await _feedBackRepository.Delete(await _feedBackRepository.Get(feedbackGuid));
        _feedBackRepository.Save();
    }
}