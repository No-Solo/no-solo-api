using NoSolo.Core.Entities.Auth;

namespace NoSolo.Abstractions.Repositories.Auth;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetActiveByUserId(Guid userId);
}