using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/userEntity-credentials")]
public class UserCredentialsController : BaseApiController
{
    private readonly IUserCredentialsService _userCredentialsService;

    public UserCredentialsController(IUserCredentialsService userCredentialsService)
    {
        _userCredentialsService = userCredentialsService;
    }

    [HttpPost("sign-up")]
    public async Task<UserDto> SignUp([FromBody] RegisterDto registerDto)
    {
        return await _userCredentialsService.SignUp(registerDto);
    }

    [HttpPost("sign-in")]
    public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
    {
        return await _userCredentialsService.SignIn(loginDto);
    }

    [HttpPut("verify-email")]
    public async Task<UserDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
    {
        return await _userCredentialsService.VerifyEmail(verificationCode);
    }

    [HttpPut("reset-password")]
    public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        return await _userCredentialsService.ResetPassword(resetPasswordDto);
    }

    [HttpPut("password")]
    public async Task UpdatePassword([FromBody] PasswordUpdateDto passwordUpdate)
    {
        await _userCredentialsService.UpdatePassword(passwordUpdate);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        return await _userCredentialsService.GetAuthorizedUser(User.GetEmail());
    }
}