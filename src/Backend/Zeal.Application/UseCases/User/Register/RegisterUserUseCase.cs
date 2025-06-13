using Zeal.Application.Services.AutoMapper;
using Zeal.Application.Services.Cryptography;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserjson> Execute(RequestRegisterUserJson request)
    {
        // Validate the request
        Validate(request);

        // Mapear Request em entidade
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        var user = autoMapper.Map<Domain.Entities.User>(request);

        // Criptografar a senha
        var cryptographyPassword = new PasswordEncrypter();

        user.Password = cryptographyPassword.Encrypt(request.Password);

        // Salvar no banco de dados

        return new ResponseRegisterUserjson
        {
            Name = request.Name
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}