namespace NoSolo.Abstractions.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string messageText);
}