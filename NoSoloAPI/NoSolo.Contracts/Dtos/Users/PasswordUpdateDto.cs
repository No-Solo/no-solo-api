namespace NoSolo.Contracts.Dtos.Users;

public class PasswordUpdateDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string OldPassword { get; set; }
}