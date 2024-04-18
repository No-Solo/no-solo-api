using System.Security.Claims;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Services.Auth;

public interface ITokenService
{
    Task<string> GenerateAccessToken(UserEntity userEntity);
    Task<string> GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}