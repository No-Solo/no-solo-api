using System.ComponentModel.DataAnnotations;

namespace NoSolo.Contracts.Dtos.Auth;

public class LoginDto
{
    [Required] public string Login { get; set; }

    [Required] public string Password { get; set; }
}