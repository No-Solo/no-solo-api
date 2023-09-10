using Core.Entities;

namespace Core.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetActiveByUserId(Guid userId);
}