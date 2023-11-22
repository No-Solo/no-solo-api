using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public class TokensDto
{
    [Required] public string AccessToken { get; set; }

    [Required] public string RefreshToken { get; set; }
}