using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Base;

public record ContactDto : BaseDto<Guid>
{
    [Required]
    public required string Type { get; init; }
    [Required]
    public required string Url { get; init; }
    [Required]
    public required string Text { get; init; }
}