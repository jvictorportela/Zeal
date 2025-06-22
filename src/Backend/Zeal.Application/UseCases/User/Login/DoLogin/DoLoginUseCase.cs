using AutoMapper;
using Zeal.Application.Services.Cryptography;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;
using Zeal.Domain.Repositories.User;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly PasswordEncrypter _passwordEncrypter;

    public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncrypter passwordEncrypter)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
    }

    public async Task<ResponseUserLogged> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncrypter.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        return new ResponseUserLogged
        {
            Name = user.Name
        };
    }
}
