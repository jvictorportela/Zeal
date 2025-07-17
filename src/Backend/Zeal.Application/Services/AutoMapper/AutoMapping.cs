using AutoMapper;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;

namespace Zeal.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        // CreateMap<de onde esta vindo os dados, qual classe vai receber os dados>
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());// ForMember(destino => destino.propriedade, como vai funcionar o mapeamento)
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
    }
}
