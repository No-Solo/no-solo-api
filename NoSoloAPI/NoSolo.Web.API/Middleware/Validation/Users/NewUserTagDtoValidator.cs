using FluentValidation;
using NoSolo.Contracts.Dtos.Users.Tags;

namespace NoSolo.Web.API.Middleware.Validation.Users;

public class NewUserTagDtoValidator : AbstractValidator<NewUserTagDto>
{
    public NewUserTagDtoValidator()
    {
        RuleFor(u => u.Active)
            .NotEmpty();
        RuleFor(u => u.Tag)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(30);
    }
}