using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Exceptions;
using ModularMonolith.Shared.Modules;
using ModularMonolith.Shared.Persistance;

namespace ModularMonolith.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModular(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddMigrationDatabase();
        services.AddSharedInfrastructure(configuration);
        services.AddModules(configuration);
        return services;
    }
}
