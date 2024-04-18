using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NoSolo.Contracts.Dtos.Base;

public record BaseDto<TKey>
{
    [Required]
    public required TKey Id { get; init; }
}

public record BaseCreatedDto<TKey> : BaseDto<TKey>
{
    [Required]
    public required bool Deleted { get; set; }

    [Required]
    public required DateTime DateCreated { get; set; }
    
    public DateTime? DateDeleted { get; set; }
}