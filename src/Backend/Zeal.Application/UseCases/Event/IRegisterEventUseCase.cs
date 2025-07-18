using Zeal.Communication.Requests.Event;
using Zeal.Communication.Responses.Event;

namespace Zeal.Application.UseCases.Event;

public interface IRegisterEventUseCase
{
    Task<ResponseRegisteredEventJson> Execute(RequestEventJson request);
}
