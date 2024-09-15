namespace ModularMonolith.Shared.Modules
{
    using Microsoft.AspNetCore.Routing;

    public interface IModuleEndpoints
    {
        void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder);
    }
}
