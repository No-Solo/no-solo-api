using NoSolo.Contracts.Dtos.FeedBack;

namespace NoSolo.Abstractions.Services.Utility;

public interface IFeedBackService
{
    Task<FeedBackDto> Create(NewFeedBackDto newFeedBackDto);
    Task<FeedBackDto> Get(Guid feedbackGuid);
    Task<IReadOnlyList<FeedBackDto>> Get();
    Task Delete(Guid feedbackGuid);
}