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
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    private readonly IUserCredentialsService _userCredentialsService;
    
    public AuthController(IAuthService authService, IUserCredentialsService userCredentialsService)
    {
        _authService = authService;
        _userCredentialsService = userCredentialsService;
    }
    
    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<TokensDto> RefreshToken([FromBody] TokensDto tokenModel)
    {
        return await _authService.RefreshToken(tokenModel);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<UserDto> GetCurrentUser()
    {
        return await _userCredentialsService.GetAuthorizedUser(User.GetEmail());
    }
}