

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Infrastructure.Services.Email.Options;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailService(IOptions<SmtpOptions> options) : IEmailService
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendEmailAsync(string email, string subject, string messageText)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(null, _options.SenderEmail));
        message.To.Add(new MailboxAddress(null, email));
        message.Subject = subject;
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = messageText
        };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_options.HostAddress, _options.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_options.SenderEmail, _options.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}