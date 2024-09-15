namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using ModularMonolith.Shared;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Security;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModular(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseRouting();
            app.UseSwagger(environment);
            if (!environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseExceptionHandler();
            app.UseSecurity();
            app.UseEndpoints(n => n.MapEndpoints());
            return app;
        }

        private static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (!environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
