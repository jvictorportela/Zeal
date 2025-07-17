using FluentValidation;
using Zeal.Communication.Requests.User;
using Zeal.Exceptions;

namespace Zeal.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.NAME_EMPTY);
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);

        When(request => string.IsNullOrWhiteSpace(request.Email) == false, () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
        });
    }
}
