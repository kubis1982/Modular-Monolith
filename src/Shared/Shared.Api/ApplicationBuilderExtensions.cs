namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Modules;

    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseModular(this WebApplication app, IWebHostEnvironment environment)
        {
            app.UseRouting();
            if (!environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseExceptionHandler();
            app.UseModules();
            app.MapEndpoints();
            return app;
        }
    }
}
