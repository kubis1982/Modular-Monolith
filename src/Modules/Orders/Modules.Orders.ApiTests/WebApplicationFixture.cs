namespace ModularMonolith.Modules.Orders
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.Orders.Persistance.ReadModel;
    using ModularMonolith.Modules.Orders.Services;

    [CollectionDefinition(nameof(WebApplicationFixtureCollection))]
    public class WebApplicationFixtureCollection : ICollectionFixture<WebApplicationFixture> { }

    public class WebApplicationFixture() : Shared.Api.WebApplicationFixture<ReadDbContext>(ModuleDefinition.MODULE_CODE)
    {
        protected override void OnServices(IServiceCollection services)
        {
            base.OnServices(services);

            services.AddScoped<IWarehousesService, ModuleServices>();
            services.AddScoped<IContractorsService, ModuleServices>();
            services.AddScoped<IArticlesService, ModuleServices>();
        }
    }
}
