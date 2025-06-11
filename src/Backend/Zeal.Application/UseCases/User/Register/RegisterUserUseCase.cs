using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    public async Task<ResponseRegisterUserjson> Execute(RequestRegisterUserJson request)
    {
        // Validate the request
        await Validate(request);

        // Mapear Request em entidade

        // Criptografar a senha

        // Salvar no banco de dados

        return new ResponseRegisterUserjson
        {
            Name = request.Name
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
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
