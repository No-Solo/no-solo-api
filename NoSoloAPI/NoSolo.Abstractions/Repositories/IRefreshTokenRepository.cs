using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Auth;

namespace NoSolo.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetActiveByUserId(Guid userId);
}