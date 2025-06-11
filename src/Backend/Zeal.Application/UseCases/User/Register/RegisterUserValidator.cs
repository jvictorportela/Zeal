using FluentValidation;
using Zeal.Communication.Requests.User;
using Zeal.Exceptions;

namespace Zeal.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.NAME_EMPTY);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);

        RuleFor(user => user.Password.Length)
            .GreaterThanOrEqualTo(6);
    }
}
