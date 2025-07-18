using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zeal.Domain.Security.Cryptography;
using Zeal.Domain.Security.Tokens;
using Zeal.Domain.Services.LoggedUser;
using Zeal.Infra.DataAccess;
using Zeal.Infra.DataAccess.Repositories;
using Zeal.Infra.Extensions;
using Zeal.Infra.Security.Cryptography;
using Zeal.Infra.Security.Tokens.Access.Generator;
using Zeal.Infra.Security.Tokens.Access.Validator;
using Zeal.Infra.Services.LoggedUser;

namespace Zeal.Infra;

public static class DependencyInjectionExtensionInfra
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddLoggedUser(services);
        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
        AddTokens(services, configuration);
        AddPasswordEncrypter(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<Domain.Repositories.User.IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.User.IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.User.IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<Domain.Repositories.IUnitOfWork, UnitOfWork>();

        services.AddScoped<Domain.Repositories.Event.IEventWriteOnlyRepository, EventRepository>();

        services.AddScoped<Domain.Repositories.Address.IAddressWriteOnlyRepository, AddressRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<ZealDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("Zeal.Infra")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");

        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var aditionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");

        services.AddScoped<IPasswordEncrypter>(options => new Sha512Encripter(aditionalKey!)
        );
    }
}