using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Modules;

namespace ModularMonolith.Modules.Articles;

public partial class ModuleDefinition : AbstractModuleDefinition
{
    public override string ModuleCode => MODULE_CODE;

    public const string MODULE_CODE = ServiceCollectionExtensions.ModuleCode;

    protected override void OnAddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModularInfrastructure(configuration);
    }
}
