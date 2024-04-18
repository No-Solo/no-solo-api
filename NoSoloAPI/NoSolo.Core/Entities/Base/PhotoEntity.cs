using System.ComponentModel.DataAnnotations;

namespace NoSolo.Core.Entities.Base;

public class PhotoEntity : BaseEntity<Guid>
{
    [Required]
    public required string Url { get; set; }
    public string? PublicId { get; set; }
}