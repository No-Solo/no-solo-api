using System.ComponentModel.DataAnnotations;

namespace NoSolo.Presentation.Dtos;

public class TokensDto
{
    [Required] public string AccessToken { get; set; }

    [Required] public string RefreshToken { get; set; }
}