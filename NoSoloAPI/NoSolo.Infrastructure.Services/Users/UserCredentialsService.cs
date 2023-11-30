using System.Security.Authentication;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Abstractions.Services.Users;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Enums;
using NoSolo.Core.Exceptions;
using NoSolo.Core.Specification.Users.Users;

namespace NoSolo.Infrastructure.Services.Users;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly INotificationService _notificationService;

    private User? _user;

    public UserCredentialsService(IMapper mapper, ITokenService tokenService,
        UserManager<User> userManager, IUnitOfWork unitOfWork, IRefreshTokenService refreshTokenService, INotificationService notificationService)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _refreshTokenService = refreshTokenService;
        _notificationService = notificationService;

        _user = null;
    }

    public async Task<UserDto> GetAuthorizedUser(string email)
    {
        _user ??= await GetUserByEmailWithAllIncludes(email);

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
                Locale = LocaleEnum.English,
                FirstName = "",
                MiddleName = null,
                LastName = "",
                About = "",
                Description = "",
                Location = "",
                Gender = GenderEnum.Other,
                Sponsorship = SponsorshipEnum.Zero
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

        _user = await GetUserByEmailWithAllIncludes(signUpDto.Email);

        await _notificationService.SendVerificationCode(signUpDto.Email);
        
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

        var refreshToken = await _refreshTokenService.GenerateRefreshToken(_user);

        return new UserAuthDto()
        {
            Tokens = new TokensDto()
            {
                AccessToken = await _tokenService.GenerateAccessToken(_user),
                RefreshToken = refreshToken.TokenHash
            },
            User = _mapper.Map<UserDto>(_user)
        };
    }

    public async Task<UserDto> VerifyEmail(VerificationCodeDto verificationCode)
    {
        var user = await Find(verificationCode.Email);

        var result = await _userManager.ConfirmEmailAsync(user, verificationCode.VerificationCode);

        if (!result.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var identityError in result.Errors)
                stringBuilder.AppendLine($"{identityError.Description}. ");

            throw new InvalidCredentialsException("Invalid verification code");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await Find(resetPasswordDto.Email);
        await _userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);

        var refreshToken = await _refreshTokenService.GenerateRefreshToken(user);

        return new UserAuthDto()
        {
            User = _mapper.Map<UserDto>(user),
            Tokens =
            {
                AccessToken = await _tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.TokenHash,
            }
        };
    }

    public async Task UpdatePassword(PasswordUpdateDto passwordUpdate)
    {
        var user = await Find(passwordUpdate.Email);

        await _userManager.ChangePasswordAsync(user, passwordUpdate.OldPassword, passwordUpdate.Password);
    }

    private async Task<User> GetUserByEmailWithAllIncludes(string email)
    {
        var userParams = new UserParams()
        {
            Email = email,
            Includes = new List<UserInclude>()
            {
                UserInclude.Contacts,
                UserInclude.Membership,
                UserInclude.Offers,
                UserInclude.Photo,
                UserInclude.Requests,
                UserInclude.Tags
            }
        };

        var spec = new UserWithSpecificationParams(userParams);

        return await _unitOfWork.Repository<User>().GetEntityWithSpec(spec);
    }

    private async Task<User> Find(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new InvalidCredentialsException("Invalid email");

        return user;
    }
}