using AutoMapper;
using Zeal.Communication.Requests.Event;
using Zeal.Communication.Responses.Event;
using Zeal.Domain.Services.LoggedUser;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.Event.Filter;

public class FilterEventUseCase : IFilterEventUseCase
{
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public FilterEventUseCase(IMapper mapper, ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseEventsJson> Execute(RequestFilterEventJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        return new ResponseEventsJson
        {
            Events = []
        };
    }

    private static void Validate(RequestFilterEventJson request)
    {
        var validator = new FilterEventValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(e => e.ErrorMessage).Distinct().ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
