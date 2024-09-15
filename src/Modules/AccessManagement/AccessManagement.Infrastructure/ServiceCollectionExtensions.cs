namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.AccessManagement.CQRS.Queries;
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using ModularMonolith.Modules.AccessManagement.Persistance.WriteModel;
    using ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Repositories;
    using ModularMonolith.Shared.Persistance;

    public static class ServiceCollectionExtensions
    {
        public const string ModuleCode = EntityType.ModuleCode;

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                // CQRS
                .AddScoped<IQueryContext, QueryContext>()
                // Repositories
                .AddScoped<IUserRepository, UserRepository>()
                // Services
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                // Persistance
                .AddReadDatabase<ReadDbContext>()
                .AddWriteDatabase<WriteDbContext>(ModuleCode);
            return services;
        }
    }
}
