using Zeal.Communication.Requests.User;
using Zeal.Domain.Repositories;
using Zeal.Domain.Repositories.User;
using Zeal.Domain.Security.Cryptography;
using Zeal.Domain.Services.LoggedUser;
using Zeal.Exceptions;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _updateOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository updateOnlyRepository, IPasswordEncrypter passwordEncripter, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _updateOnlyRepository = updateOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request, loggedUser);

        var user = await _updateOnlyRepository.GetById(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _updateOnlyRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);

        var currentPasswordEncrypted = _passwordEncripter.Encrypt(request.CurrentPassword);

        if (!currentPasswordEncrypted.Equals(loggedUser.Password))
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.PASSWORD_INVALID));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
