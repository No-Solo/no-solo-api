namespace NoSolo.Abstractions.Services.Email;

public interface IVerificationCodeService
{
    Task<string> GeneratePasswordLink(string email);
    Task<string> GenerateVerificationEmailCode(string email);
}