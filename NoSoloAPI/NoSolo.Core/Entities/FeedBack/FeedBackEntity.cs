using System.ComponentModel.DataAnnotations;
using NoSolo.Core.Entities.Base;

namespace NoSolo.Core.Entities.FeedBack;

public class FeedBackEntity : BaseEntity<Guid>
{
    [Required]
    public required string FirstName { get; init; }
    [Required]
    public required string LastName { get; init; }
    [Required]
    public required string Email { get; init; }
    [Required]
    public required string FeedBackText { get; init; }
}