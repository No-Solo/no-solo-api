using FluentValidation;
using NoSolo.Contracts.Dtos.Contacts;

namespace NoSolo.Web.API.Middleware.Validation.Contacts;

public class NewContactDtoValidator : AbstractValidator<NewContactDto>
{
    public NewContactDtoValidator()
    {
        RuleFor(u => u.Text)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(u => u.Type)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(u => u.Url)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}