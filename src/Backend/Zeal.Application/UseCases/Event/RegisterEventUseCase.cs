using AutoMapper;
using Zeal.Communication.Requests.Event;
using Zeal.Communication.Responses.Event;
using Zeal.Domain.Repositories;
using Zeal.Domain.Repositories.Event;
using Zeal.Domain.Services.LoggedUser;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.Event;

public class RegisterEventUseCase : IRegisterEventUseCase
{
    private readonly IEventWriteOnlyRepository _eventWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterEventUseCase(IEventWriteOnlyRepository eventWriteOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _eventWriteOnlyRepository = eventWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredEventJson> Execute(RequestEventJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var eventEntity = _mapper.Map<Domain.Entities.Event>(request);
        eventEntity.UserId = loggedUser.Id;

        await _eventWriteOnlyRepository.Add(eventEntity); 
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredEventJson>(eventEntity);
    }

    private static void Validate(RequestEventJson request)
    {
        var result = new EventValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }
}
