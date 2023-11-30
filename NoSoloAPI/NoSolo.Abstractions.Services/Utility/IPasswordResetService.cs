namespace NoSolo.Abstractions.Services.Utility;

public interface IPasswordResetService
{
    Task<string> GeneratePasswordResetCode(string email);
}