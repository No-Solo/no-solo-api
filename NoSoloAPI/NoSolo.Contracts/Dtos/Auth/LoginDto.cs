using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public record LoginDto
{
    [Required] public required string Login { get; init; }

    [Required] public required string Password { get; init; }
}