namespace ModularMonolith.Modules.Ordering
{

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.Articles;
    using ModularMonolith.Modules.Contractors;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
    using ModularMonolith.Modules.Ordering.Persistance.WriteModel;
    using ModularMonolith.Modules.Ordering.Persistance.WriteModel.Repositories;
    using ModularMonolith.Modules.Ordering.Services;
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
