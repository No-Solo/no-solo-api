namespace NoSolo.Contracts.Dtos.Auth;

public class ResetPasswordDto
{
    public string Code { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}