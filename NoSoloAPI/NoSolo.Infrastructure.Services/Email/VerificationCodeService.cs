using NoSolo.Abstractions.Services.Email;
using NoSolo.Abstractions.Services.Utility;

namespace NoSolo.Infrastructure.Services.Email;

public class VerificationCodeService : IVerificationCodeService
{
    private readonly IEmailTokenService _emailTokenService;
    private readonly IPasswordResetService _passwordResetService;

    public VerificationCodeService(IEmailTokenService emailTokenService, IPasswordResetService passwordResetService)
    {
        _emailTokenService = emailTokenService;
        _passwordResetService = passwordResetService;
    }
    
    public async Task<string> GeneratePasswordLink(string email)
    {
        return await _passwordResetService.GeneratePasswordResetCode(email);
    }

    public async Task<string> GenerateVerificationEmailCode(string email)
    {
        return await _emailTokenService.Generate(email);
    }
}