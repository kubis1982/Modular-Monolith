namespace ModularMonolith.Shared.Api
{
    using DotNet.Testcontainers.Configurations;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using Respawn;
    using System.Threading.Tasks;
    using Testcontainers.PostgreSql;
    using Xunit;

    /// <summary>
    /// Fixture for setting up a web application for testing purposes.
    /// </summary>
    /// <param name="moduleCode">The module code for the application.</param>
    public class WebApplicationFixture(string moduleCode) : IAsyncLifetime {
        private PostgreSqlContainer dbContainer = default!;
        private Respawner respawner = default!;

        /// <summary>
        /// Static constructor to initialize the database container and respawner.
        /// </summary>
        static WebApplicationFixture() {
            TestcontainersSettings.ResourceReaperEnabled = Environment.GetEnvironmentVariable("CI") != "true";
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; } = default!;

        /// <summary>
        /// Gets the HTTP test client.
        /// </summary>
        public TestHttpClient HttpClient { get; private set; } = default!;

        /// <summary>
        /// Allows derived classes to configure services.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        protected virtual void OnServices(IServiceCollection services) {
        }

        /// <summary>
        /// Creates an HTTP client for the web application factory.
        /// </summary>
        /// <param name="webApplicationFactory">The web application factory.</param>
        /// <returns>A configured HTTP client.</returns>
        private static HttpClient CreateHttpClient(WebApplicationFactory webApplicationFactory) {
            HttpClient client = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false,
            });
            return client;
        }

        /// <summary>
        /// Initializes the fixture asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        async Task IAsyncLifetime.InitializeAsync() {
            dbContainer = new PostgreSqlBuilder().WithImage("postgres:15.8").Build();
            await dbContainer.StartAsync();
            var webApplicationFactory = new WebApplicationFactory(dbContainer.GetConnectionString(), OnServices, true);
            HttpClient client = CreateHttpClient(webApplicationFactory);
            HttpClient = new TestHttpClient(moduleCode, client);
            ServiceProvider = webApplicationFactory.Services;
            using var dbConnection = new NpgsqlConnection(dbContainer.GetConnectionString());
            dbConnection.Open();
            respawner = await Respawner.CreateAsync(dbConnection, new RespawnerOptions() {
                DbAdapter = DbAdapter.Postgres,
                WithReseed = true
            });            
        }

        /// <summary>
        /// Disposes the fixture asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        async Task IAsyncLifetime.DisposeAsync() {
            await dbContainer.DisposeAsync().AsTask();
            HttpClient.Dispose();
        }

        /// <summary>
        /// Creates a new HTTP test client with additional service configuration.
        /// </summary>
        /// <param name="action">An action to configure additional services.</param>
        /// <returns>A new instance of <see cref="TestHttpClient"/>.</returns>
        public TestHttpClient CreateHttpClient(Action<IServiceCollection> action) {
            var webApplicationFactory = new WebApplicationFactory(dbContainer.GetConnectionString(), n => {
                OnServices(n);
                action.Invoke(n);
            });
            HttpClient client = CreateHttpClient(webApplicationFactory);
            return new TestHttpClient(moduleCode, client);
        }

        /// <summary>
        /// Resets the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ResetDatabaseAsync() {
            using var conn = new NpgsqlConnection(dbContainer.GetConnectionString());
            await conn.OpenAsync();
            await respawner.ResetAsync(conn);
        }
    }

    /// <summary>
    /// Fixture for setting up a web application for testing purposes with a specific DbContext.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="moduleCode">The module code for the application.</param>
    public class WebApplicationFixture<TDbContext>(string moduleCode) : WebApplicationFixture(moduleCode) where TDbContext : DbContext {        
    }
}
