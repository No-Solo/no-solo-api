namespace NoSolo.Abstractions.Services.Email;

public interface IEmailTokenService
{
    Task<string> Generate(string userEmail);
}