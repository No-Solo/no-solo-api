using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.FeedBack;

public record FeedBackDto : BaseDto<Guid>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string FeedBackText { get; init; }
}