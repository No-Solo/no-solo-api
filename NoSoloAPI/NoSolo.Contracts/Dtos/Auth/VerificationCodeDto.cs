using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public record VerificationCodeDto
{
    [Required]
    public required string Email { get; init; }
    [Required]
    public required string VerificationCode { get; init; }
}