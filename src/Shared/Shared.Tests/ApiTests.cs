namespace ModularMonolith.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Api;
    using System;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Abstract base class for API tests, providing common setup and utility methods.
    /// </summary>
    /// <param name="webApplicationFixture">The factory to create the web API client.</param>
    /// <param name="testOutputHelper">The helper to output test results.</param>
    [Trait("Category", "Api")]
    public abstract class ApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : IAsyncLifetime
    {
        private TestHttpClient testHttpClient = webApplicationFixture.HttpClient;

        /// <summary>
        /// Gets the HTTP test client, with logging configured.
        /// </summary>
        protected TestHttpClient HttpClient
        {
            get
            {
                testHttpClient.Log = TestOutputHelper.WriteLine;
                return testHttpClient;
            }
        }

        /// <summary>
        /// Gets the test output helper.
        /// </summary>
        protected ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;

        /// <summary>
        /// Changes the services used by the HTTP client.
        /// </summary>
        /// <param name="action">An action to configure the service collection.</param>
        protected void ChangeServices(Action<IServiceCollection> action) => testHttpClient = webApplicationFixture.CreateHttpClient(action);

        /// <inheritdoc />
        public virtual Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task InitializeAsync()
        {
            await webApplicationFixture.ResetDatabaseAsync();
        }

        private readonly Lazy<IServiceProvider> serviceProvider = new(() => webApplicationFixture.ServiceProvider.CreateScope().ServiceProvider);

        public IServiceProvider Services => serviceProvider.Value;
    }

    /// <summary>
    /// Abstract base class for API tests with a specific database context, providing common setup and utility methods.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public abstract class ApiTests<TDbContext> : ApiTests where TDbContext : DbContext
    {
        private readonly Lazy<TDbContext> dbContext;

        protected ApiTests(WebApplicationFixture<TDbContext> webApplicationFixture, ITestOutputHelper testOutputHelper) : base(webApplicationFixture, testOutputHelper)
            => dbContext = new(() => Services.GetRequiredService<TDbContext>());

        /// <summary>
        /// Gets a database context.
        /// </summary>
        protected TDbContext DbContext => dbContext.Value;
    }
}
