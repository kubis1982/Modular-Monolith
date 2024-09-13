namespace Kubis1982.Shared.Serialization
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSerialization(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            return services;
        }
    }
}
