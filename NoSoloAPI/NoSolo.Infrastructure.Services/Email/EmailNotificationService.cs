using Microsoft.Extensions.Configuration;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailNotificationService : INotificationService
{
    private readonly IEmailTokenService _emailTokenService;
    private readonly IEmailService _emailService;

    public EmailNotificationService(IEmailTokenService emailTokenService, IEmailService emailService)
    {
        _emailTokenService = emailTokenService;
        _emailService = emailService;
    }
    
    public Task SendPasswordResetLink(string userEmail)
    {
        throw new NotImplementedException();
    }

    public async Task SendVerificationCode(string userEmail)
    {
        var emailToken = await _emailTokenService.Generate(userEmail);
        await _emailService.SendEmail(userEmail, "Verify your email", $"Your verification code is {userEmail}");
    }
}