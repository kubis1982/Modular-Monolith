namespace ModularMonolith.Modules.ReadModel {
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.ReadModel.Exceptions;
    using ModularMonolith.Shared.Modules;
    using System;

    internal abstract class ModuleEndpoints(Modules module) : IModuleEndpoints {
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder) {
            string moduleCode = module.GetModulePrefixFromEnumValue();
            endpointRouteBuilder = endpointRouteBuilder.MapGroup(moduleCode.ToLower());
            ModuleEndpointRouteBuilder moduleEndpointRouteBuilder = new(module, endpointRouteBuilder);
            AddRoutes(moduleEndpointRouteBuilder);
        }

        public abstract void AddRoutes(IModuleEndpointRouteBuilder endpointRouteBuilder);
    }
}
