using System.Text;
using Microsoft.AspNetCore.Identity;
using NoSolo.Abstractions.Services.Auth;
using NoSolo.Abstractions.Services.Email;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;

namespace NoSolo.Infrastructure.Services.Email;

public class EmailTokenService(UserManager<UserEntity> userManager) : IEmailTokenService
{
    public async Task<string> Generate(string userEmail)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user is null)
            throw new InvalidCredentialsException("Invalid userEntity's email");

        var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        return emailToken;
    }
}