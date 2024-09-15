namespace ModularMonolith.Shared.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.Linq;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var module in AppDomain.CurrentDomain.GetSystemAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(AbstractModuleDefinition).IsAssignableFrom(x) && !x.IsAbstract)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<AbstractModuleDefinition>()
                .Distinct())
            {
                module.AddDependencies(services, configuration);
                services.AddSingleton(module);
                services.AddSingleton(module.GetType(), module);

                module.GetType().Assembly.GetTypes()
                    .Where(x => typeof(IModuleEndpoints).IsAssignableFrom(x) && !x.IsAbstract)
                    .OrderBy(x => x.Name)
                    .ToList().ForEach(x =>
                    {
                        services.AddTransient(x);
                        services.AddKeyedTransient(module.ModuleCode, (provider, key) => (IModuleEndpoints)provider.GetService(x)!);
                    });
            }
            return services;
        }
    }
}
