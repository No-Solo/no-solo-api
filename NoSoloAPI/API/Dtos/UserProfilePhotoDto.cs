namespace API.Dtos;

public class UserProfilePhotoDto : BaseDto
{
    public string Url { get; set; }
    public string? PublicId { get; set; }
}