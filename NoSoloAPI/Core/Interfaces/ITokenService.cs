using System.Security.Claims;
using Core.Entities;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}