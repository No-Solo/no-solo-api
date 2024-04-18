using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly ITokenService _tokenService;
    private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

    public RefreshTokenService(ITokenService tokenService, IGenericRepository<RefreshToken> refreshTokenRepository)
    {
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public async Task<RefreshToken> GenerateRefreshToken(UserEntity userEntity)
    {
        var refreshToken = new RefreshToken
        {
            TokenHash = await _tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            UserEntity = userEntity
        };

        _refreshTokenRepository.AddAsync(refreshToken);
        _refreshTokenRepository.Save();

        return refreshToken;
    }
}