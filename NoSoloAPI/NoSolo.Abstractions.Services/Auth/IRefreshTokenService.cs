using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Services.Auth;

public interface IRefreshTokenService
{
    Task<RefreshToken> GenerateRefreshToken(User user);
}