using Zeal.Communication.Requests.User;

namespace Zeal.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
