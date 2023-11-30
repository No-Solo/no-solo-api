using System.Text;
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
}