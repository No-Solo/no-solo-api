using NoSolo.Core.Entities.Auth;

namespace NoSolo.Abstractions.Repositories.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetActiveByUserId(Guid userId);
}