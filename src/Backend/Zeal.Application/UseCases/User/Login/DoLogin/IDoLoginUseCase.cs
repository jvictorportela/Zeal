using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;

namespace Zeal.Application.UseCases.User.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseUserLogged> Execute(RequestLoginJson request);
}
