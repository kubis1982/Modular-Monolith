using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Warehouses.Api;
using ModularMonolith.Modules.Warehouses.Persistance.ReadModel;
using ModularMonolith.Modules.Warehouses.Persistance.WriteModel;
using ModularMonolith.Modules.Warehouses.Persistance.WriteModel.Repositories;
using ModularMonolith.Shared.Persistance;

namespace ModularMonolith.Modules.Warehouses;

public static class ServiceCollectionExtensions
{
    public const string ModuleCode = EntityType.ModuleCode;
    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            // Api
            .AddScoped<IWarehousesModuleApi, WarehousesModuleApi>()
            // Repositories           
            .AddScoped<IWarehouseRepository, WarehouseRepository>()
            // Services            
            .AddScoped<IWarehouseUsageService, WarehouseUsageService>()
            // Persistance
            .AddReadDatabase<ReadDbContext>()
            .AddWriteDatabase<WriteDbContext>(EntityType.ModuleCode);
        return services;
    }
}