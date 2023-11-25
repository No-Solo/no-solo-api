using NoSolo.Contracts.Dtos.Base;

namespace NoSolo.Contracts.Dtos.Users.Photo;

public class UserPhotoDto : BaseDto
{
    public string Url { get; set; }
    public string? PublicId { get; set; }
}