using Kubis1982.Shared.CQRS.Commands;
using Kubis1982.Shared.CQRS.Queries;
using Kubis1982.Shared.Extensions;
using Kubis1982.Shared.Pipelines;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kubis1982.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetSystemAssemblies());
            config.AutoRegisterRequestProcessors = false;

            services.Scan(scan => scan
                .FromAssemblies(AppDomain.CurrentDomain.GetSystemAssemblies())
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestPreProcessor<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
               .FromAssemblies(AppDomain.CurrentDomain.GetSystemAssemblies())
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestPostProcessor<,>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
        });
        services.AddScoped<ICommandExecutor, CommandExecutor>()
            .AddScoped<IQueryExecutor, QueryExecutor>();
        return services;
    }
}
