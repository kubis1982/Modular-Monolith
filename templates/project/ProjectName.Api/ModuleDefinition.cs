namespace ModularMonolith.Modules.ProjectName
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Security;
    using System;
    using System.Text;

    internal class ModuleDefinition : AbstractModuleDefinition
    {
        public override string ModuleCode => ServiceCollectionExtensions.ModuleCode;

        public override string ModuleName => "ProjectName";

        protected override void OnAddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddModularInfrastructure(configuration);
        }
    }
}
