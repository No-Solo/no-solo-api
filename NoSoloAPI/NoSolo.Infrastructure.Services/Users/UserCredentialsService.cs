﻿using System.Security.Authentication;
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

public class UserCredentialsService(
    IMapper mapper,
    ITokenService tokenService,
    UserManager<UserEntity> userManager,
    IUnitOfWork unitOfWork,
    IRefreshTokenService refreshTokenService,
    INotificationService notificationService)
    : IUserCredentialsService
{
    private UserEntity? _user = null;

    public async Task<UserDto> GetAuthorizedUser(string email)
    {
        _user ??= await GetUserByEmailWithAllIncludes(email);

        return mapper.Map<UserDto>(_user);
    }

    public async Task<UserDto> SignUp(RegisterDto signUpDto)
    {
        if (_user is not null)
            throw new AuthenticationException("You are authorized");

        if (await userManager.FindByEmailAsync(signUpDto.Email) is not null ||
            await userManager.FindByNameAsync(signUpDto.UserName) is not null)
            throw new ExistingAccountException();

        var result = await userManager.CreateAsync(
            new UserEntity()
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

        await notificationService.SendVerificationCode(signUpDto.Email);
        
        return mapper.Map<UserDto>(_user);
    }

    public async Task<UserAuthDto> SignIn(LoginDto login)
    {
        if (_user is not null)
            throw new AuthException("You are authorized");

        _user = await userManager.FindByEmailAsync(login.Login);

        if (_user is null)
            _user = await userManager.FindByNameAsync(login.Login);
        if (_user is null || !await userManager.CheckPasswordAsync(_user, login.Password))
            throw new InvalidCredentialsException("Invalid password or login");

        var refreshToken = await refreshTokenService.GenerateRefreshToken(_user);

        return new UserAuthDto()
        {
            Tokens = new TokensDto()
            {
                AccessToken = await tokenService.GenerateAccessToken(_user),
                RefreshToken = refreshToken.TokenHash
            },
            User = mapper.Map<UserDto>(_user)
        };
    }

    public async Task<UserDto> VerifyEmail(VerificationCodeDto verificationCode)
    {
        var user = await Find(verificationCode.Email);

        var result = await userManager.ConfirmEmailAsync(user, verificationCode.VerificationCode);

        if (!result.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var identityError in result.Errors)
                stringBuilder.AppendLine($"{identityError.Description}. ");

            throw new InvalidCredentialsException("Invalid verification code");
        }

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await Find(resetPasswordDto.Email);
        await userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);

        var refreshToken = await refreshTokenService.GenerateRefreshToken(user);

        return new UserAuthDto()
        {
            User = mapper.Map<UserDto>(user),
            Tokens = new TokensDto()
            {
                AccessToken = await tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.TokenHash,
            }
        };
    }

    public async Task UpdatePassword(PasswordUpdateDto passwordUpdate)
    {
        var user = await Find(passwordUpdate.Email);

        await userManager.ChangePasswordAsync(user, passwordUpdate.OldPassword, passwordUpdate.Password);
    }

    private async Task<UserEntity> GetUserByEmailWithAllIncludes(string email)
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

        return await unitOfWork.Repository<UserEntity>().GetEntityWithSpec(spec);
    }

    private async Task<UserEntity> Find(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            throw new InvalidCredentialsException("Invalid email");

        return user;
    }
}