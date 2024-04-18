using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public record ResetPasswordDto
{
    [Required]
    public required string Code { get; init; }
    [Required]
    public required string Password { get; init; }
    [Required]
    public required string Email { get; init; }
}