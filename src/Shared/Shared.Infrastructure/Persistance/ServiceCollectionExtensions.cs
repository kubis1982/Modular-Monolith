namespace Kubis1982.Shared.Persistance
{
    using Kubis1982.Shared.Extensions;
    using Kubis1982.Shared.Persistance.Interceptors;
    using Kubis1982.Shared.Persistance.ReadModel;
    using Kubis1982.Shared.Persistance.WriteModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;

    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DbOptionsSetup>();
            services.AddScoped<IWriteDbContextProvider, WriteDbContextProvider>();
            services.AddSingleton<IInterceptor, PostgresExceptionProcessorInterceptor>();
            return services;
        }

        private static string GetConnectionString<T>(IServiceProvider serviceProvider, bool read = false)
        {
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<DbOptions>>();
            string? connectionString = options.CurrentValue.ConnectionString;
            if (read)
            {
                connectionString = options.CurrentValue.ReadConnectionString ?? connectionString;
            }
            return connectionString ?? throw new ArgumentException("Nie zdefiniowano połączenia do bazy danych");
        }

        public static IServiceCollection AddWriteDatabase<T>(this IServiceCollection services, string schema) where T : WriteDbContextBase
        {
            services.AddDbContext<T>(x =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                var interceptors = serviceProvider.GetRequiredService<IEnumerable<IInterceptor>>();
                x.AddInterceptors(interceptors);
                x.UseNpgsql(GetConnectionString<T>(serviceProvider), n =>
                {
                    n.MigrationsHistoryTable("MigrationHistory", schema);
                });
#if DEBUG
                x.ConfigureWarnings(
                    b => b.Log(
                        (RelationalEventId.TransactionCommitted, LogLevel.Information),
                        (RelationalEventId.TransactionStarted, LogLevel.Information),
                        (RelationalEventId.TransactionRolledBack, LogLevel.Information)));

                x.LogTo(s => System.Diagnostics.Debug.WriteLine(s), (eventId, logLevel) => logLevel >= LogLevel.Information
                                       || eventId == RelationalEventId.TransactionStarted
                                       || eventId == RelationalEventId.TransactionCommitted
                                       || eventId == RelationalEventId.TransactionRolledBack)
                 .EnableDetailedErrors(true)
                 .EnableSensitiveDataLogging(true);
#endif
            });
            string moduleName = typeof(T).GetModuleName();
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork<T>>(moduleName);
            services.AddKeyedScoped<T>(moduleName, (n, m) => n.GetRequiredService<T>());
            return services;
        }

        public static IServiceCollection AddReadDatabase<T>(this IServiceCollection services) where T : ReadDbContextBase
        {
            services.AddDbContext<T>(x =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                x.UseNpgsql(GetConnectionString<T>(serviceProvider, true));
#if DEBUG
                x.LogTo(s => System.Diagnostics.Debug.WriteLine(s), LogLevel.Information)
                 .EnableDetailedErrors(true)
                 .EnableSensitiveDataLogging(true);
#endif
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}
