namespace ModularMonolith.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ModularMonolith.Shared.Persistance;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using System;

    /// <summary>
    /// Represents a factory for creating test helpers.
    /// </summary>
    /// <typeparam name="TEntryPoint">The type of the entry point.</typeparam>
    /// <typeparam name="TWriteDbContext">The type of the write database context.</typeparam>
    /// <typeparam name="TReadDbContext">The type of the read database context.</typeparam>
    public abstract class TestHelperFactory<TEntryPoint, TWriteDbContext, TReadDbContext>(string routePrefix, bool modulating = false, bool deleted = true) : ITestHelperFactory where TEntryPoint : class where TWriteDbContext : WriteDbContextBase where TReadDbContext : ReadDbContextBase
    {
        /// <summary>
        /// Gets or sets the log action.
        /// </summary>
        public Action<string>? Log { get; set; }

        private static void Migrate<TDbContext>(TestWebApplicationFactory<TEntryPoint> factory, bool deleted) where TDbContext : WriteDbContextBase
        {
            using var scope = factory.Services.CreateScope();
            using var writeDbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            if (deleted)
            {
                writeDbContext.Database.EnsureDeleted();
            }
            writeDbContext.Database.Migrate();
        }

        private TestHelper CreateTestHelper(bool migration, TestWebApplicationFactory<TEntryPoint> factory)
        {
            if (migration)
            {
                Migrate<TWriteDbContext>(factory, deleted);
                if (modulating)
                {
                    DbMigrationHelper.MigrateAsync(factory.Services, factory.Services.GetRequiredService<ILogger<TestHelper>>(), default).Wait();
                }
            }
            TestHttpClient httpApi = TestHttpClient.CreateHttpApi(routePrefix, factory, Log);
            httpApi.Disposed += () =>
            {
                if (deleted)
                {
                    using var scope = factory.Services.CreateScope();
                    using var writeDbContext = scope.ServiceProvider.GetRequiredService<TWriteDbContext>();
                    writeDbContext.Database.EnsureDeleted();
                }
            };
            ITestDbApi dbApi = CreateDbApi(factory);
            return new TestHelper(httpApi, dbApi, factory.Services);
        }

        /// <summary>
        /// Creates a test helper.
        /// </summary>
        /// <param name="migration">A flag indicating whether to perform database migration.</param>
        /// <param name="serviceCollection">An optional action to configure the service collection.</param>
        /// <returns>The created test helper.</returns>
        public virtual TestHelper CreateTestHelper(bool migration, Action<IServiceCollection>? serviceCollection = null)
        {
            var factory = new TestWebApplicationFactory<TEntryPoint>(serviceCollection);
            return CreateTestHelper(migration, factory);
        }

        private static TestDbApi<TReadDbContext> CreateDbApi(TestWebApplicationFactory<TEntryPoint> factory) => new(factory.Services);
    }
}
