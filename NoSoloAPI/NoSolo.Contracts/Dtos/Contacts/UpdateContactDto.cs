using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Contacts;

public class UpdateContactDto
{
    [Required]
    public required string Type { get; init; }
    
    [Required]
    public required string Url { get; init; }
    
    [Required]
    public required string Text { get; init; }
}