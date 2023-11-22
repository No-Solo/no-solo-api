using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Core.Entities.Auth;

namespace NoSolo.Abstractions.Services.Auth;

public interface IAuthService
{
    Task SendVerificationCode(string email);

    Task<TokensDto> RefreshToken(TokensDto expiredToken);
    Task SendResetPassword(string email);
}