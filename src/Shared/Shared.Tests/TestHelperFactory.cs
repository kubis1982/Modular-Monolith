namespace ModularMonolith.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ModularMonolith.Shared.Persistance;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using System;

    public abstract class TestHelperFactory<TEntryPoint, TWriteDbContext, TReadDbContext>(string routePrefix, bool modulating = false, bool deleted = true) : ITestHelperFactory where TEntryPoint : class where TWriteDbContext : WriteDbContextBase where TReadDbContext : ReadDbContextBase
    {
        public Action<string>? Log { get; set; }

        private static void Migrate<TDbContext>(CustomWebApplicationFactory<TEntryPoint> factory, bool deleted) where TDbContext : WriteDbContextBase
        {
            using var scope = factory.Services.CreateScope();
            using var writeDbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            if (deleted)
            {
                writeDbContext.Database.EnsureDeleted();
            }
            writeDbContext.Database.Migrate();
        }

        private TestHelper CreateTestHelper(bool migration, CustomWebApplicationFactory<TEntryPoint> factory)
        {
            if (migration)
            {
                Migrate<TWriteDbContext>(factory, deleted);
                if (modulating)
                    DbMigrationHelper.MigrateAsync(factory.Services, factory.Services.GetRequiredService<ILogger<TestHelper>>(), default).Wait();
            }
            HttpTestClient httpApi = HttpTestClient.CreateHttpApi(routePrefix, factory, Log);
            httpApi.Disposed += () =>
            {
                if (deleted)
                {
                    using var scope = factory.Services.CreateScope();
                    using var writeDbContext = scope.ServiceProvider.GetRequiredService<TWriteDbContext>();
                    writeDbContext.Database.EnsureDeleted();
                }
            };
            IDbApi dbApi = CreateDbApi(factory);
            return new TestHelper(httpApi, dbApi, factory.Services);
        }

        public virtual TestHelper CreateTestHelper(bool migration, Action<IServiceCollection>? serviceCollection = null)
        {
            var factory = new CustomWebApplicationFactory<TEntryPoint>(serviceCollection);
            return CreateTestHelper(migration, factory);
        }

        private static IDbApi CreateDbApi(CustomWebApplicationFactory<TEntryPoint> factory) => new DbApi<TReadDbContext>(factory.Services);
    }
}
