namespace ModularMonolith.Shared.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class DbMigrationHelper
    {
        public static async Task MigrateAsync(IServiceProvider serviceProvider, ILogger logger, CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetSystemAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(WriteDbContextBase).IsAssignableFrom(x) && !x.IsInterface && x != typeof(WriteDbContextBase));

            using var scope = serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                WriteDbContextBase? dbContext = scope.ServiceProvider.GetService(dbContextType) as WriteDbContextBase;
                if (dbContext is not null)
                {
                    logger.LogInformation("Running DB context for module `{Module}`...", dbContextType.GetModuleName());
                    await dbContext.Database.MigrateAsync(cancellationToken);
                }
            }
        }
    }
}
