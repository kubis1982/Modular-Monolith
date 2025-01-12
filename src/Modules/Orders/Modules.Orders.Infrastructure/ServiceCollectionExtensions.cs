namespace ModularMonolith.Modules.Orders
{

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.Articles;
    using ModularMonolith.Modules.Contractors;
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using ModularMonolith.Modules.Orders.Persistance.ReadModel;
    using ModularMonolith.Modules.Orders.Persistance.WriteModel;
    using ModularMonolith.Modules.Orders.Persistance.WriteModel.Repositories;
    using ModularMonolith.Modules.Orders.Services;
    using ModularMonolith.Modules.Warehouses;
    using ModularMonolith.Shared.Persistance;

    public static class ServiceCollectionExtensions
    {
        public const string ModuleCode = EntityType.ModuleCode;

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                // Repositories
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IDocumentNumberGenerator, DocumentNumberGenerator>()
                // Services
                .AddScoped<IWarehousesService, WarehousesService>()
                .AddScoped<IArticlesService, ArticlesService>()
                .AddScoped<IContractorsService, ContractorsService>()
                .AddScoped<IArticleUsageChecker, ArticleUsageChecker>()
                .AddScoped<IWarehouseUsageChecker, WarehouseUsageChecker>()
                .AddScoped<IContractorUsageChecker, ContractorUsageChecker>()
                // Persistance
                .AddReadDatabase<ReadDbContext>()
                .AddWriteDatabase<WriteDbContext>(ModuleCode);

            return services;
        }
    }
}
