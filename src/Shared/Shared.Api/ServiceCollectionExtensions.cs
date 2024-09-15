using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Documentation;
using ModularMonolith.Shared.Modules;
using ModularMonolith.Shared.Security;

namespace ModularMonolith.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModular(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();
        services.AddEndpointsApiDocumentation();
        services.AddSecurity(configuration);
        services.AddSharedInfrastructure(configuration);
        services.AddModules(configuration);
        return services;
    }
}
