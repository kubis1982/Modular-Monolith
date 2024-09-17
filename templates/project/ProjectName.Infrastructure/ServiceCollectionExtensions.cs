namespace ModularMonolith.Modules.ProjectName
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.ProjectName.Persistance.ReadModel;
    using ModularMonolith.Modules.ProjectName.Persistance.WriteModel;
    using ModularMonolith.Modules.ProjectName.Domain;
    using ModularMonolith.Shared.Persistance;

    public static class ServiceCollectionExtensions
    {
        public const string ModuleCode = EntityType.ModuleCode;

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                // Persistance
                .AddReadDatabase<ReadDbContext>()
                .AddWriteDatabase<WriteDbContext>(ModuleCode);
            return services;
        }
    }
}
