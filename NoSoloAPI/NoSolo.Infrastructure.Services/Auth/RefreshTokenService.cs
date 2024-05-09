using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Auth;

public class RefreshTokenService(ITokenService tokenService, IRepository<RefreshToken> refreshTokenRepository)
    : IRefreshTokenService
{
    public async Task<RefreshToken> GenerateRefreshToken(UserEntity userEntity)
    {
        var refreshToken = new RefreshToken
        {
            TokenHash = await tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            UserEntity = userEntity
        };

        refreshTokenRepository.AddAsync(refreshToken);
        refreshTokenRepository.Save();

        return refreshToken;
    }
}