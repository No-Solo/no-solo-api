using FluentValidation;
using NoSolo.Contracts.Dtos.FeedBack;

namespace NoSolo.Web.API.Middleware.Validation.FeedBack;

public class NewFeedBackDtoValidator : AbstractValidator<NewFeedBackDto>
{
    public NewFeedBackDtoValidator()
    {
        RuleFor(f => f.FeedBackText)
            .MinimumLength(1)
            .MaximumLength(500)
            .NotEmpty();

        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(f => f.FirstName)
            .NotEmpty();

        RuleFor(l => l.LastName)
            .NotEmpty();
    }
}