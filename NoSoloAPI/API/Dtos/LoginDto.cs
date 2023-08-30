using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class LoginDto
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}