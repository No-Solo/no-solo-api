namespace NoSolo.Abstractions.Services.Email;

public interface INotificationService
{
    Task SendPasswordResetLink(string userEmail);
    Task SendVerificationCode(string userEmail);
}