using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using NoSolo.Abstractions.Services.Email;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailNotificationService(
    IVerificationCodeService verificationCodeService,
    IEmailService emailService,
    IConfiguration configuration)
    : INotificationService
{
    public async Task SendPasswordResetLink(string userEmail)
    {
        var code = await verificationCodeService.GeneratePasswordLink(userEmail);
        var link = configuration["ResetPasswordLink"];
        
        var param = new Dictionary<string, string>() { { "email", userEmail }, { "code", code } };

        var url = new Uri(QueryHelpers.AddQueryString(link, param));
        
        await emailService.SendEmailAsync(userEmail, "Follow the link to reset your password", $"Follow the link to reset your password: {url}");
    }

    public async Task SendVerificationCode(string userEmail)
    {
        var emailToken = await verificationCodeService.GenerateVerificationEmailCode(userEmail);
        
        var link = configuration["VerificationCodeLink"];
        
        var param = new Dictionary<string, string>() { { "email", userEmail }, { "code", emailToken } };

        var url = new Uri(QueryHelpers.AddQueryString(link, param));
        
        await emailService.SendEmailAsync(userEmail, "Verify your email", $"Your verification code is {url}");
    }
}