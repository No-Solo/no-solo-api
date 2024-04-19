using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Controllers;

[AllowAnonymous]
[Route("api/userEntity-info")]
[ExcludeFromCodeCoverage]
public class UserInfoController : BaseApiController
{
    private readonly IUserService _userService;

    public UserInfoController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPut]
    public async Task<UserDto> UpdateUserInfo(UpdateUserDto updateUserDto)
    {
        return await _userService.Update(updateUserDto, User.GetEmail());
    }

    [HttpGet("{email}")]
    public async Task<UserDto> GetUserInfo(string email)
    {
        return await _userService.Get(email);
    }
}