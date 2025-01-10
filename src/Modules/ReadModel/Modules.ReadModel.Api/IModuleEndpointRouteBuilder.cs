namespace ModularMonolith.Modules.ReadModel {
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing.Patterns;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IModuleEndpointRouteBuilder {
        IEndpointConventionBuilder MapGet([StringSyntax("Route")] string pattern, Delegate @delegate);
        IModuleEndpointRouteBuilder MapGroup(RoutePattern prefix);
    }
}