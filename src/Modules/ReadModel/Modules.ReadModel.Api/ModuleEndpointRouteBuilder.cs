namespace ModularMonolith.Modules.ReadModel {
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Routing.Patterns;
    using System;
    using System.Diagnostics.CodeAnalysis;

    internal class ModuleEndpointRouteBuilder(Modules module, IEndpointRouteBuilder endpointRouteBuilder) : IModuleEndpointRouteBuilder {
        public IModuleEndpointRouteBuilder MapGroup(RoutePattern prefix) {
            return new ModuleEndpointRouteBuilder(module, endpointRouteBuilder.MapGroup(prefix));
        }

        public IEndpointConventionBuilder MapGet([StringSyntax("Route")] string pattern, Delegate @delegate) {
            return endpointRouteBuilder.MapGet(pattern, @delegate)
                .WithName($"ReadModel{module}{@delegate.Method.Name}");
        }
    }
}
