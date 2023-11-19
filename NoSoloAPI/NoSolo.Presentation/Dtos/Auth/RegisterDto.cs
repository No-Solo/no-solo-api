using System.ComponentModel.DataAnnotations;

namespace NoSolo.Presentation.Dtos;

public class RegisterDto
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }
}