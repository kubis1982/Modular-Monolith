using Kubis1982.Shared.CQRS.Commands;
using Kubis1982.Shared.CQRS.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kubis1982.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICommandExecutor, CommandExecutor>()
            .AddScoped<IQueryExecutor, QueryExecutor>();
        return services;
    }
}