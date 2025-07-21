using Zeal.Communication.Requests.Event;
using Zeal.Communication.Responses.Event;

namespace Zeal.Application.UseCases.Event.Filter;

public interface IFilterEventUseCase
{
    Task<ResponseEventsJson> Execute(RequestFilterEventJson request);
} 