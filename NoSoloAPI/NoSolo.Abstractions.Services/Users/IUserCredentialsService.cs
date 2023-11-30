using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Users;

namespace NoSolo.Abstractions.Services.Users;

public interface IUserCredentialsService
{
    Task<UserDto> GetAuthorizedUser(string email);
    Task<UserDto> SignUp(RegisterDto signUpDto);
    Task<UserAuthDto> SignIn(LoginDto login);
    Task<UserDto> VerifyEmail(VerificationCodeDto verificationCode);
    Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task UpdatePassword(PasswordUpdateDto passwordUpdate);
}