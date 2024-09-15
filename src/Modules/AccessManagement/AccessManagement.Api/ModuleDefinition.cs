namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.AccessManagement.Services;
    using ModularMonolith.Shared.Modules;

    internal class ModuleDefinition : AbstractModuleDefinition
    {
        public override string ModuleCode => ServiceCollectionExtensions.ModuleCode;

        public override string ModuleName => "Access Management";

        public override void AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISessionService, SessionService>();
            services.AddModularInfrastructure(configuration);
        }
    }
}
