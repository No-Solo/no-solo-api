using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthorizationController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public AuthorizationController(IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = await _userManager.FindByNameAsync(registerDto.UserName);

        if (user != null) return Conflict(new { Message = "User already exists" });

        var result = await _userManager.CreateAsync(
            new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            },
            registerDto.Password
        );
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "User created" });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password)) return Unauthorized();

        var refreshToken = new RefreshToken
        {
            TokenHash = await _tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            User = user
        };
        _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
        await _unitOfWork.Complete();

        return Ok(new TokensDto
        {
            AccessToken = await _tokenService.GenerateAccessToken(user),
            RefreshToken = refreshToken.TokenHash
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokensDto tokenModel)
    {
        var principals = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        var user = await _userManager.FindByNameAsync(principals.Identity.Name);
        if (user == null) return Unauthorized();

        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetActiveByUserId(user.Id);
        if (refreshToken == null) return Unauthorized();

        return Ok(new
        {
            AccessToken = await _tokenService.GenerateAccessToken(user)
        });
    }
}