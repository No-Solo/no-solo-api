using System.ComponentModel.DataAnnotations;

namespace NoSolo.Presentation.Dtos;

public class LoginDto
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}