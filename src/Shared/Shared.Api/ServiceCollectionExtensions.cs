using ModularMonolith.Shared.Documentation;
using ModularMonolith.Shared.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModular(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();
        services.AddEndpointsApiDocumentation();
        services.AddHttpContextAccessor();
        services.AddSecurity(configuration);
        services.AddSharedInfrastructure(configuration);
        return services;
    }
}
