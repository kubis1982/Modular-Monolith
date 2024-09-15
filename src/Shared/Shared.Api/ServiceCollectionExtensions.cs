using ModularMonolith.Shared.Documentation;
using ModularMonolith.Shared.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Modules;

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
