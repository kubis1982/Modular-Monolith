namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Represents a factory for creating test web applications.
    /// </summary>
    /// <typeparam name="TEntryPoint">The type of the entry point for the web application.</typeparam>
    public class TestWebApplicationFactory<TEntryPoint>(Action<IServiceCollection>? serviceCollection = null) : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        /// <summary>
        /// Configures the web host builder for the test web application.
        /// </summary>
        /// <param name="builder">The web host builder.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            if (serviceCollection is not null)
            {
                builder.ConfigureServices(serviceCollection);
            }
        }
    }
}
