using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Articles.Api;
using ModularMonolith.Modules.Articles.Persistance.ReadModel;
using ModularMonolith.Modules.Articles.Persistance.WriteModel;
using ModularMonolith.Modules.Articles.Persistance.WriteModel.Repositories;
using ModularMonolith.Shared.Persistance;

namespace ModularMonolith.Modules.Articles;

public static class ServiceCollectionExtensions
{
    public const string ModuleCode = EntityType.ModuleCode;
    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            // Api
            .AddScoped<IArticlesModuleApi, ArticlesModuleApi>()
            // Repositories
            .AddScoped<IArticleRepository, ArticleRepository>()
            .AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>()
            // Services
            .AddScoped<IArticleUsageService, ArticleUsageService>()
            // Persistance
            .AddReadDatabase<ReadDbContext>()
            .AddWriteDatabase<WriteDbContext>(EntityType.ModuleCode);

        return services;
    }
}
