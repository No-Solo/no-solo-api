using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Infrastructure.Services.Email.Options;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _options;

    public EmailService(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }
    
    public async Task SendEmail(string to, string subject, string message)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_options.SenderEmail);
        if (!string.IsNullOrEmpty(_options.SenderName))
        {
            email.Sender.Name = _options.SenderName;
        }
        email.From.Add(email.Sender);
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = message };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_options.HostAddress, _options.Port, _options.SecureSocketOptions);
            smtp.Authenticate(_options.Username, _options.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}