using System.Security.Claims;
using NoSolo.Core.Entities;
using NoSolo.Core.Entities.User;

namespace NoSolo.Abstractions.Services;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}