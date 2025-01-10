namespace ModularMonolith.Modules.ReadModel
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using ModularMonolith.Shared.Persistance;

    public static class ServiceCollectionExtensions {
        public const string MODULE_CODE = "RmM";

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services
                // Read model
                .AddReadDatabase<ReadDbContext>();
            return services;
        }
    }
}
