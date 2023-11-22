namespace NoSolo.Abstractions.Services.Email;

public interface IEmailService
{
    Task SendEmail(string to, string subject, string message);
}