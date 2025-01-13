namespace ModularMonolith.Modules.ReadModel
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.OData;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Modules;

    public partial class ModuleDefinition : AbstractModuleDefinition
    {
        public const string MODULE_CODE = ServiceCollectionExtensions.MODULE_CODE;
        public override string ModuleCode => MODULE_CODE;

        protected override void OnAddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IActionFilter, EnableQueryAttribute>();
            services.AddControllers(n => n.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddOData(n => n.Select().Filter().OrderBy().Expand().SetMaxTop(null).Count());
            services.AddModularInfrastructure(configuration);
        }

        protected override void OnUseServices(WebApplication app)
        {
            base.OnUseServices(app);

            app.UseODataRouteDebug();
            app.MapControllers();
        }
    }
}
