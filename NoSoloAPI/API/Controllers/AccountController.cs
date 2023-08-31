using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AccountController(IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<User> userManager,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = await _userManager.FindByNameAsync(registerDto.UserName);

        if (user != null) return Conflict(new ApiResponse(409, "User already exists"));

        var result = await _userManager.CreateAsync(
            new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            },
            registerDto.Password
        );

        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400, result.Errors.ToString()));

        return Ok(new ApiResponse(200, "User created"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized(new ApiResponse(401));

        var refreshToken = new RefreshToken
        {
            TokenHash = await _tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            User = user
        };

        _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

        if (await _unitOfWork.Complete())
            return Ok(new TokensDto
            {
                AccessToken = await _tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.TokenHash
            });

        return BadRequest(new ApiResponse(400, "Problem"));
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken([FromBody] TokensDto tokenModel)
    {
        var principals = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        var user = await _userManager.FindByNameAsync(principals.Identity.Name);
        if (user == null) return Unauthorized();

        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetActiveByUserId(user.Id);
        if (refreshToken == null) 
            return Unauthorized(new ApiResponse(401));

        return Ok(new
        {
            AccessToken = await _tokenService.GenerateAccessToken(user)
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(User.GetUsername());

        var userToReturn = _mapper.Map<UserDto>(user);

        return Ok(userToReturn);
    }
}