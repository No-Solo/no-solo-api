using NoSolo.Abstractions.Services.Email;
using NoSolo.Abstractions.Services.Utility;

namespace NoSolo.Infrastructure.Services.Email;

public class VerificationCodeService(IEmailTokenService emailTokenService, IPasswordResetService passwordResetService)
    : IVerificationCodeService
{
    public async Task<string> GeneratePasswordLink(string email)
    {
        return await passwordResetService.GeneratePasswordResetCode(email);
    }

    public async Task<string> GenerateVerificationEmailCode(string email)
    {
        return await emailTokenService.Generate(email);
    }
}