namespace ModularMonolith.Shared.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ApplicationBuilderExtensions
    {
        public static WebApplication UseModules(this WebApplication app)
        {
            foreach (var module in app.Services.GetServices<AbstractModuleDefinition>())
            {
                module.UseServices(app);
            }
            return app;
        }

    }
}
