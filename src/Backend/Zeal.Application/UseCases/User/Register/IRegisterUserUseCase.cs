using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;

namespace Zeal.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserjson> Execute(RequestRegisterUserJson request);
}
