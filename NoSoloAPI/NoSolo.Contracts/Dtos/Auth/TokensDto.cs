using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public record TokensDto
{
    [Required] public required string AccessToken { get; set; }

    [Required] public required string RefreshToken { get; set; }
}