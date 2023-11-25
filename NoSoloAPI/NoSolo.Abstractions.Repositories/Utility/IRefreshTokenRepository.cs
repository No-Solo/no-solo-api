using NoSolo.Core.Entities.Auth;

namespace NoSolo.Abstractions.Repositories.Utility;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetActiveByUserId(Guid userId);
}