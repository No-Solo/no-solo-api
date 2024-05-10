using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/userEntity-credentials")]
[ExcludeFromCodeCoverage]
public class UserCredentialsController(IUserCredentialsService userCredentialsService) : BaseApiController
{
    [HttpPost("sign-up")]
    public async Task<UserDto> SignUp([FromBody] RegisterDto registerDto)
    {
        return await userCredentialsService.SignUp(registerDto);
    }

    [HttpPost("sign-in")]
    public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
    {
        return await userCredentialsService.SignIn(loginDto);
    }

    [HttpPut("verify-email")]
    public async Task<UserDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
    {
        return await userCredentialsService.VerifyEmail(verificationCode);
    }

    [HttpPut("reset-password")]
    public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        return await userCredentialsService.ResetPassword(resetPasswordDto);
    }

    [HttpPut("password")]
    public async Task UpdatePassword([FromBody] PasswordUpdateDto passwordUpdate)
    {
        await userCredentialsService.UpdatePassword(passwordUpdate);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        return await userCredentialsService.GetAuthorizedUser(User.GetEmail());
    }
}