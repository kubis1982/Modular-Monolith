namespace ModularMonolith.Shared.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Modules;
    using System.Linq;

    public static class EndpointRouteBuilderExtensions
    {
        internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            AbstractModuleDefinition[] modules = builder.ServiceProvider.GetServices<AbstractModuleDefinition>().ToArray();
            foreach (var module in modules) {
                IModuleEndpoints[] endpoints = builder.ServiceProvider.GetKeyedServices<IModuleEndpoints>(module.ModuleCode).ToArray();
                var group = builder.MapGroup(module.ModuleCode.ToLowerInvariant()).WithTags(module.ModuleName);                
                foreach (var endpoint in endpoints)
                {
                    endpoint.AddRoutes(group);
                }
            }
            return builder;
        }
    }
}
