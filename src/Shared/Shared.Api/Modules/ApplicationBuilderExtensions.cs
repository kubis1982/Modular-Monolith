namespace ModularMonolith.Shared.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModules(this IApplicationBuilder app)
        {
            foreach (var module in app.ApplicationServices.GetServices<AbstractModuleDefinition>())
            {
                module.UseServices(app);
            }
            return app;
        }

    }
}
