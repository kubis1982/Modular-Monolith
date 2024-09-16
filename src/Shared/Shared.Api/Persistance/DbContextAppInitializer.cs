namespace ModularMonolith.Shared.Persistance
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DbContextAppInitializer(IServiceProvider serviceProvider, ILogger<DbContextAppInitializer> logger, IOptions<DbOptions> options) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (options.Value.Migrator.IsEnabled == true)
            {
                await DbMigrationHelper.MigrateAsync(serviceProvider, logger, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
