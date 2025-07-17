using Zeal.Communication.Responses.User;

namespace Zeal.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
