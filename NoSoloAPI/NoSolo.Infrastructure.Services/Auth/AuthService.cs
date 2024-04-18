using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Repositories.Auth;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly INotificationService _notificationService;

    public AuthService(ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository,
        UserManager<UserEntity> userManager, INotificationService notificationService)
    {
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _notificationService = notificationService;
    }

    public async Task SendVerificationCode(string email)
    {
        await _notificationService.SendVerificationCode(email);
    }

    public async Task<TokensDto> RefreshToken(TokensDto expiredToken)
    {
        var principals = _tokenService.GetPrincipalFromExpiredToken(expiredToken.AccessToken);
        var user = await _userManager.FindByNameAsync(principals.Identity.Name);
        if (user is null)
            throw new InvalidCredentialsException("Invalid userEntity");

        var refreshToken = await _refreshTokenRepository.GetActiveByUserId(user.Id);
        if (refreshToken is null)
            throw new InvalidCredentialsException("InvalidRefreshToken");

        return new TokensDto()
        {
            AccessToken = await _tokenService.GenerateAccessToken(user),
            RefreshToken = refreshToken.TokenHash
        };
    }

    public Task SendResetPassword(string email)
    {
        throw new NotImplementedException();
    }
}