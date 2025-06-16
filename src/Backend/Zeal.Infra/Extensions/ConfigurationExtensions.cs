using Microsoft.Extensions.Configuration;

namespace Zeal.Infra.Extensions;

public static class ConfigurationExtensions
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("Connection")!;
    }
}
