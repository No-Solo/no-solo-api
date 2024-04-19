using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Validation;

namespace NoSolo.Web.API.Middleware.Validation.Auth;

[ExcludeFromCodeCoverage]
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(u => u.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .Must(p => p.IsValidPassword());
    }
}