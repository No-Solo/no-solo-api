using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public record RegisterDto
{
    [Required] public required string UserName { get; init; }

    [Required] public required string Password { get; init; }

    [Required] [EmailAddress] public required string Email { get; init; }
}