using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Kubis1982.Shared.Security;

public static class ServiceCollectionExtensions {
    internal static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration) { 
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddScoped(n => n.GetRequiredService<IUserContextAccessor>().Get());
        return services;
    }

    internal static IApplicationBuilder UseSecurity(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.UseAuthentication();
        applicationBuilder.UseAuthorization();
        return applicationBuilder;
    }
}
