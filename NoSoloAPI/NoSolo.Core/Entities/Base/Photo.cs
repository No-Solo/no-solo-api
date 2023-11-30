namespace NoSolo.Core.Entities.Base;

public class Photo : BaseEntity
{
    public string Url { get; set; }
    public string? PublicId { get; set; }
}