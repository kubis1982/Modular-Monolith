namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class CustomWebApplicationFactory<TEntryPoint>(Action<IServiceCollection>? serviceCollection = null) : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
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
