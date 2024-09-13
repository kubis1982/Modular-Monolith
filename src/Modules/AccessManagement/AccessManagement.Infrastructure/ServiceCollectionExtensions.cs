namespace Kubis1982.Modules.AccessManagement
{
    using Kubis1982.Modules.AccessManagement.Persistance.ReadModel;
    using Kubis1982.Modules.AccessManagement.Persistance.WriteModel;
    using Kubis1982.Modules.AccessManagement.Persistance.WriteModel.Repositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Kubis1982.Shared.Persistance;

    public static class ServiceCollectionExtensions
    {
        public const string ModuleCode = EntityType.ModuleCode;

        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
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
