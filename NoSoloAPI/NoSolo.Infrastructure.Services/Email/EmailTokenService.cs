﻿using System.Text;
using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailTokenService : IEmailTokenService
{
    private readonly UserManager<User> _userManager;

    public EmailTokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<string> Generate(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null)
            throw new InvalidCredentialsException("Invalid user's email");
        
        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return emailToken;
    }

    public async Task<bool> Verify(string userEmail, string emailToken)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user is null)
            throw new InvalidCredentialsException("Invalid user's email");
        
        var result = await _userManager.ConfirmEmailAsync(user, emailToken);

        if (!result.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var identityError in result.Errors)
                stringBuilder.AppendLine(identityError.Description);

            throw new InvalidCredentialsException(stringBuilder.ToString());
        }
        
        return result.Succeeded;
    }
}