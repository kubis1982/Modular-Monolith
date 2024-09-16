namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Persistance;
    using ModularMonolith.Shared.Security;
    using System.Diagnostics.CodeAnalysis;
    using Xunit.Abstractions;

    [Trait("Category", "Api")]
    [ExcludeFromCodeCoverage]
    public abstract class ApiTests<T>(ITestOutputHelper testOutputHelper) : IAsyncLifetime where T : class, ITestHelperFactory, new()
    {
        public TestHelper TestHelper { get; private set; } = null!;

        public Task DisposeAsync()
        {
            TestHelper.Dispose();
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            AddServices(true);
            return Task.CompletedTask;
        }

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
                n.AddSingleton<UserContextTest>();
                n.AddScoped<IUserContext, UserContextTest>();
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

        protected virtual void OnAddServices(IServiceCollection serviceCollection)
        {
        }

        private static string GetConnectionString(string moduleName)
        {
            return $"User ID=postgres;Password=mypassword;Host=localhost;Port=5432;Database={SystemInformation.SystemName}_Modules_{moduleName}";
        }
    }
}
