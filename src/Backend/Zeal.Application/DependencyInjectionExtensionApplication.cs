using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Application.UseCases.User.Register;

namespace Zeal.Application;

public static class DependencyInjectionExtensionApplication
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
