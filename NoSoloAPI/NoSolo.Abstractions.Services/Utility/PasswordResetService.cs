using Microsoft.AspNetCore.Identity;
using NoSolo.Core.Entities.User;
using NoSolo.Core.Exceptions;

namespace NoSolo.Abstractions.Services.Utility;

public class PasswordResetService : IPasswordResetService
{
    private readonly UserManager<UserEntity> _userManager;

    public PasswordResetService(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<string> GeneratePasswordResetCode(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new InvalidCredentialsException("Invalid email");
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}