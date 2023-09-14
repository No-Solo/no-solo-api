using System.Security.Claims;
using Core.Entities;

namespace Core.Interfaces.Serivces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}