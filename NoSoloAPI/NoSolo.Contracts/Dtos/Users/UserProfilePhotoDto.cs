using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.User;

public class UserProfilePhotoDto : BaseDto
{
    public string Url { get; set; }
    public string? PublicId { get; set; }
}