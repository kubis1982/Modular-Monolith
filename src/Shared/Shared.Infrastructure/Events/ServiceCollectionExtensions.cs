using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Events.Domain;
using ModularMonolith.Shared.Extensions;
using System;
using System.Linq;

namespace ModularMonolith.Shared.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();

            Type eventCommandType = typeof(EventCommand<>);

            foreach (var assembly in AppDomain.CurrentDomain.GetSystemAssemblies())
            {
                foreach (var type in assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == eventCommandType))
                {
                    services.AddSingleton(new Subscriber(type.GetModuleName(), type.GetProperty(nameof(EventCommand<IIntegrationEvent>.Event))!.PropertyType));
                }
            }

            return services;
        }
    }
}
