using System.Security.Authentication;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Repositories.Repositories;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.Users;

namespace NoSolo.Infrastructure.Services.Users;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    private User? _user;

    public UserCredentialsService(IMapper mapper, ITokenService tokenService, IUserRepository userRepository,
        UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _userManager = userManager;
        _unitOfWork = unitOfWork;

        _user = null;
    }

    public async Task<UserDto> GetAuthorizedUser(string email)
    {
        _user = await GetUserByEmailWithAllIncludes(email);

        return _mapper.Map<UserDto>(_user);
    }

    public async Task<UserDto> SignUp(RegisterDto signUpDto)
    {
        if (_user is not null)
            throw new AuthenticationException("You are authorized");

        if (await _userManager.FindByEmailAsync(signUpDto.Email) is not null ||
            await _userManager.FindByNameAsync(signUpDto.UserName) is not null)
            throw new ExistingAccountException();

        var result = await _userManager.CreateAsync(
            new User()
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                UserProfile = null
            },
            signUpDto.Password
        );

        if (!result.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var identityError in result.Errors)
                stringBuilder.AppendLine(identityError.Description);

            throw new InvalidCredentialsException(stringBuilder.ToString());
        }

        _user = await _userRepository.GetUserByEmailWithAllIncludesAsync(signUpDto.Email);

        return _mapper.Map<UserDto>(_user);
    }

    public async Task<UserAuthDto> SignIn(LoginDto login)
    {
        if (_user is not null)
            throw new AuthException("You are authorized");

        _user = await _userManager.FindByEmailAsync(login.Login);

        if (_user is null)
            _user = await _userManager.FindByNameAsync(login.Login);
        if (_user is null || !await _userManager.CheckPasswordAsync(_user, login.Password))
            throw new InvalidCredentialsException("Invalid password or login");

        var refreshToken = new RefreshToken
        {
            TokenHash = await _tokenService.GenerateRefreshToken(),
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            User = _user
        };

        _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

        if (await _unitOfWork.Complete())
            return new UserAuthDto()
            {
                Tokens =
                {
                    AccessToken = await _tokenService.GenerateAccessToken(_user),
                    RefreshToken = refreshToken.TokenHash
                },
                User = _mapper.Map<UserDto>(_user)
            };

        return null!;
    }

    private async Task<User> GetUserByEmailWithAllIncludes(string email)
    {
        var userParams = new UserParams()
        {
            Email = email,
            OrganizationsInclude = true,
            UserProfileInclude = true
        };
        
        var spec = new UserWithSpecificationParams(userParams);

        return await _unitOfWork.Repository<User>().GetEntityWithSpec(spec);
    }
}