using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.CQRS.Commands;
using ModularMonolith.Shared.CQRS.Queries;
using ModularMonolith.Shared.Extensions;
using ModularMonolith.Shared.Pipelines;
using System;

namespace ModularMonolith.Shared.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
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
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();
            return services;
        }
    }
}
