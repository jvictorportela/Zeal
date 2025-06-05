using FluentValidation;
using Zeal.Communication.Requests.User;

namespace Zeal.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("The name cannot be empty!");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("The email cannot be empty!")
            .EmailAddress()
            .WithMessage("The email is invalid");

        RuleFor(user => user.Password.Length)
            .GreaterThanOrEqualTo(6);
    }
}
