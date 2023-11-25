namespace NoSolo.Contracts.Dtos.Users.Tags;

public class UpdateUserTagDto
{
    public Guid Guid { get; set; }
    public string Tag { get; set; }
    public bool Active { get; set; }
}