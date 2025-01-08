namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Modules;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModular(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseRouting();
            if (!environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseExceptionHandler();
            app.UseModules();
            app.UseEndpoints(n => n.MapEndpoints());
            return app;
        }
    }
}
