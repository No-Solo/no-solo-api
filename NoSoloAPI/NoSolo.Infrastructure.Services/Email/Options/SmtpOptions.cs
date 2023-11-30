using MailKit.Security;

namespace NoSolo.Infrastructure.Services.Email.Options;

public class SmtpOptions
{
    public string HostAddress { get; set; } = string.Empty;

    public int Port { get; set; } = 587;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;

    public string SenderEmail { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;
}