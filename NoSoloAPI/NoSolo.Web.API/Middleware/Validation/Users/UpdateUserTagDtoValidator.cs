using FluentValidation;
using NoSolo.Contracts.Dtos.Users.Tags;

namespace NoSolo.Web.API.Middleware.Validation.Users;

public class UpdateUserTagDtoValidator : AbstractValidator<UpdateUserTagDto>
{
    public UpdateUserTagDtoValidator()
    {
        RuleFor(u => u.Tag)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(30);

        RuleFor(u => u.Active)
            .NotEmpty();

        RuleFor(u => u.Guid)
            .NotEmpty();
    }
}