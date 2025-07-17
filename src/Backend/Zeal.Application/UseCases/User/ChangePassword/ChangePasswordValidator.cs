using FluentValidation;
using Zeal.Application.SharedValidators;
using Zeal.Communication.Requests.User;

namespace Zeal.Application.UseCases.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
