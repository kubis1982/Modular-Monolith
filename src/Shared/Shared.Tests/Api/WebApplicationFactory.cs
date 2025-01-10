namespace ModularMonolith.Shared.Api
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Api.Security;
    using ModularMonolith.Shared.Security;

    /// <summary>
    /// A custom WebApplicationFactory for setting up the test environment.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="WebApplicationFactory"/> class with the specified connection string.
    /// </remarks>
    /// <param name="connectionString">The connection string to use for the database.</param>
    /// <param name="action"></param>
    /// <param name="migrate"></param>
    internal class WebApplicationFactory(string connectionString, Action<IServiceCollection> action, bool migrate = false) : WebApplicationFactory<StartBootstraper>
    {
        /// <summary>
        /// Configures the web host with custom services and configuration settings.
        /// </summary>
        /// <param name="builder">The <see cref="IWebHostBuilder"/> to configure.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IUserContext, UserContextTest>();

                action?.Invoke(services);
            });

            builder.ConfigureAppConfiguration((context, config) =>
            {
                var settings = new Dictionary<string, string?>
                {
                    { "ModularMonolith:Db:ConnectionString", connectionString },
                    { "ModularMonolith:Db:ReadConnectionString", connectionString },
                    { "ModularMonolith:Db:Migrator:IsEnabled", migrate.ToString() }
                };
                config.AddInMemoryCollection(settings);
            });
        }
    }
}
