namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Persistance;
    using ModularMonolith.Shared.Security;
    using System.Diagnostics.CodeAnalysis;
    using Xunit.Abstractions;

    /// <summary>
    /// Base class for API tests.
    /// </summary>
    /// <typeparam name="T">The type of the test helper factory.</typeparam>
    /// <param name="testOutputHelper">The test output helper.</param>
    /// <seealso cref="IAsyncLifetime"/>
    [Trait("Category", "Api")]
    [ExcludeFromCodeCoverage]
    public abstract class ApiTests<T>(ITestOutputHelper testOutputHelper) : IAsyncLifetime where T : class, ITestHelperFactory, new()
    {
        /// <summary>
        /// Gets or sets the test helper.
        /// </summary>
        public TestHelper TestHelper { get; private set; } = null!;

        /// <inheritdoc/>
        public Task DisposeAsync()
        {
            ((IDisposable)TestHelper).Dispose();
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task InitializeAsync()
        {
            AddServices(true);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Changes the services.
        /// </summary>
        /// <param name="action">The action to modify the service collection.</param>
        public void ChangeServices(Action<IServiceCollection>? action = null)
        {
            AddServices(false, action);
        }

        private void AddServices(bool migration, Action<IServiceCollection>? action = null)
        {
            T testHelperFactory = new()
            {
                Log = testOutputHelper.WriteLine
            };
            TestHelper = testHelperFactory.CreateTestHelper(migration, n =>
            {
                n.AddSingleton<TestUserContext>();
                n.AddScoped<IUserContext, TestUserContext>();
                n.Configure<DbOptions>(options =>
                {
                    string moduleName = GetType().GetModuleName();
                    options.ConnectionString = ApiTests<T>.GetConnectionString(moduleName);
                    options.ReadConnectionString = ApiTests<T>.GetConnectionString(moduleName);
                    options.Migrator.IsEnabled = false;
                });
                OnAddServices(n);
                action?.Invoke(n);
            });
        }

        /// <summary>
        /// Method to add additional services to the service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        protected virtual void OnAddServices(IServiceCollection serviceCollection)
        {
        }

        private static string GetConnectionString(string moduleName)
        {
            return $"User ID=postgres;Password=mypassword;Host=localhost;Port=5432;Database={SystemInformation.SystemName}_Modules_{moduleName}";
        }
    }
}
