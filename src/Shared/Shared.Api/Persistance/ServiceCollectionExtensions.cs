namespace ModularMonolith.Shared.Persistance
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrationDatabase(this IServiceCollection services)
        {
            services.AddHostedService<DbContextAppInitializer>();
            return services;
        }
    }
}
