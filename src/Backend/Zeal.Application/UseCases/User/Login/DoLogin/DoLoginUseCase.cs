using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;
using Zeal.Domain.Repositories.User;
using Zeal.Domain.Security.Cryptography;
using Zeal.Domain.Security.Tokens;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncrypter passwordEncrypter, 
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseUserLogged> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncrypter.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        return new ResponseUserLogged
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
