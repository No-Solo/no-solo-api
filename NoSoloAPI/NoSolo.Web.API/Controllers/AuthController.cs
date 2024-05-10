using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/auth")]
[ExcludeFromCodeCoverage]
public class AuthController(IAuthService authService, IUserCredentialsService userCredentialsService)
    : BaseApiController
{
    [HttpPost("send-code/{email}")]
    public async Task ResendVerificationCode(string email)
    {
        await authService.SendVerificationCode(email);
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<TokensDto> RefreshToken([FromBody] TokensDto tokenModel)
    {
        return await authService.RefreshToken(tokenModel);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<UserDto> GetCurrentUser()
    {
        return await userCredentialsService.GetAuthorizedUser(User.GetEmail());
    }
}