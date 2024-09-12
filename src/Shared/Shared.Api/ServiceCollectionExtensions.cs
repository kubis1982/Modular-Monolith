using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kubis1982.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModular(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddSharedInfrastructure(configuration);
        return services;
    }
}