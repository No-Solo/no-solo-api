using NoSolo.Abstractions.Services.Email;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailNotificationService : INotificationService
{
    private readonly IVerificationCodeService _verificationCodeService;
    private readonly IEmailService _emailService;

    public EmailNotificationService(IVerificationCodeService verificationCodeService, IEmailService emailService)
    {
        _verificationCodeService = verificationCodeService;
        _emailService = emailService;
    }

    public async Task SendPasswordResetLink(string userEmail)
    {
        // var code = await _verificationCodeService.GeneratePasswordLink(userEmail);
    }

    public async Task SendVerificationCode(string userEmail)
    {
        var emailToken = await _verificationCodeService.GenerateVerificationEmailCode(userEmail);
        await _emailService.SendEmailAsync(userEmail, "Verify your email", $"Your verification code is {emailToken}");
    }
}