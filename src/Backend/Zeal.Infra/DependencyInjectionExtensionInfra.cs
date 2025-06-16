using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeal.Infra.DataAccess;
using Zeal.Infra.DataAccess.Repositories;
using Zeal.Infra.Extensions;

namespace Zeal.Infra;

public static class DependencyInjectionExtensionInfra
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<Domain.Repositories.User.IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.User.IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.User.IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<Domain.Repositories.IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<ZealDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connectionString!));
        });
    }
}