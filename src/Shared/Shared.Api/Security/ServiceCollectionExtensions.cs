using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModularMonolith.Shared.Security;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddScoped(n => n.GetRequiredService<IUserContextAccessor>().Get());
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }

    internal static IApplicationBuilder UseSecurity(this IApplicationBuilder applicationBuilder)
    {
        //applicationBuilder.UseAuthentication();
        //applicationBuilder.UseAuthorization();
        return applicationBuilder;
    }
}
