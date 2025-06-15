using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Application.Services.AutoMapper;
using Zeal.Application.Services.Cryptography;
using Zeal.Application.UseCases.User.Register;

namespace Zeal.Application;

public static class DependencyInjectionExtensionApplication
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services, configuration);
        AddPasswordEncrypter(services, configuration);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var aditionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");

        services.AddScoped(options => new PasswordEncrypter(aditionalKey!)
        );
    }
}
