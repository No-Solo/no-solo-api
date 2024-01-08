using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using NoSolo.Abstractions.Services.Email;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailNotificationService : INotificationService
{
    private readonly IVerificationCodeService _verificationCodeService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public EmailNotificationService(IVerificationCodeService verificationCodeService, IEmailService emailService, IConfiguration configuration)
    {
        _verificationCodeService = verificationCodeService;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task SendPasswordResetLink(string userEmail)
    {
        var code = await _verificationCodeService.GeneratePasswordLink(userEmail);
        var link = _configuration["ResetPasswordLink"];
        
        var param = new Dictionary<string, string>() { { "email", userEmail }, { "code", code } };

        var url = new Uri(QueryHelpers.AddQueryString(link, param));
        await _emailService.SendEmailAsync(userEmail, "Follow the link to reset your password", $"Follow the link to reset your password: {url}");
    }

    public async Task SendVerificationCode(string userEmail)
    {
        var emailToken = await _verificationCodeService.GenerateVerificationEmailCode(userEmail);
        
        var link = _configuration["VerificationCodeLink"];
        
        var param = new Dictionary<string, string>() { { "email", userEmail }, { "code", emailToken } };

        var url = new Uri(QueryHelpers.AddQueryString(link, param));
        
        await _emailService.SendEmailAsync(userEmail, "Verify your email", $"Your verification code is {url}");
    }
}