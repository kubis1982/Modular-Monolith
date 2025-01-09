﻿namespace ModularMonolith.Modules.Ordering
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Modules;

    public partial class ModuleDefinition : AbstractModuleDefinition
    {
        public const string MODULE_CODE = ServiceCollectionExtensions.ModuleCode;

        public override string ModuleCode => MODULE_CODE;

        protected override void OnAddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddModularInfrastructure(configuration);
        }
    }
}