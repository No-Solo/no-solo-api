namespace NoSolo.Contracts.Dtos.Users;

public class PasswordUpdateDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string OldPassword { get; set; }
}