using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Repositories.Auth;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Auth;

public class AuthService(
    ITokenService tokenService,
    IRefreshTokenRepository refreshTokenRepository,
    UserManager<UserEntity> userManager,
    INotificationService notificationService)
    : IAuthService
{
    public async Task SendVerificationCode(string email)
    {
        await notificationService.SendVerificationCode(email);
    }

    public async Task<TokensDto> RefreshToken(TokensDto expiredToken)
    {
        var principals = tokenService.GetPrincipalFromExpiredToken(expiredToken.AccessToken);
        var user = await userManager.FindByNameAsync(principals.Identity.Name);
        if (user is null)
            throw new InvalidCredentialsException("Invalid userEntity");

        var refreshToken = await refreshTokenRepository.GetActiveByUserId(user.Id);
        if (refreshToken is null)
            throw new InvalidCredentialsException("InvalidRefreshToken");

        return new TokensDto()
        {
            AccessToken = await tokenService.GenerateAccessToken(user),
            RefreshToken = refreshToken.TokenHash
        };
    }

    public Task SendResetPassword(string email)
    {
        throw new NotImplementedException();
    }
}