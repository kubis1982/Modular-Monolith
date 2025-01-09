using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Contractors.Api;
using ModularMonolith.Modules.Contractors.Persistance.ReadModel;
using ModularMonolith.Modules.Contractors.Persistance.WriteModel;
using ModularMonolith.Modules.Contractors.Persistance.WriteModel.Repositories;
using ModularMonolith.Shared.Persistance;

namespace ModularMonolith.Modules.Contractors;

public static class ServiceCollectionExtensions
{
    public const string ModuleCode = EntityType.ModuleCode;
    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            // Repositories
            .AddScoped<IContractorRepository, ContractorRepository>()
            // Api
            .AddScoped<IContractorsModuleApi, ContractorsModuleApi>()
            // Services
            .AddScoped<IContractorUsageService, ContractorUsageService>()
            // Persistance
            .AddReadDatabase<ReadDbContext>()
            .AddWriteDatabase<WriteDbContext>(EntityType.ModuleCode);

        return services;
    }
}
