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
        services.ConfigureOptions<CorsOptionsSetup>();
        services.AddCorsPolicy(configuration);
        return services;
    }

    internal static IApplicationBuilder UseSecurity(this IApplicationBuilder applicationBuilder) {
        applicationBuilder.UseCors("CORS");
        applicationBuilder.UseAuthentication();
        applicationBuilder.UseAuthorization();
        return applicationBuilder;
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration) {
        IConfigureOptions<CorsOptions> corsOptionsSetup = new CorsOptionsSetup(configuration);
        CorsOptions corsOptions = new();
        corsOptionsSetup.Configure(corsOptions);

        return services
            .AddCors(cors => {
                var allowedHeaders = corsOptions.AllowedHeaders ?? Enumerable.Empty<string>();
                var allowedMethods = corsOptions.AllowedMethods ?? Enumerable.Empty<string>();
                var allowedOrigins = corsOptions.AllowedOrigins ?? Enumerable.Empty<string>();
                var exposedHeaders = corsOptions.ExposedHeaders ?? Enumerable.Empty<string>();
                cors.AddPolicy("CORS", corsBuilder => {
                    var origins = allowedOrigins.ToArray();
                    if (corsOptions.AllowCredentials && origins.FirstOrDefault() != "*") {
                        corsBuilder.AllowCredentials();
                    } else {
                        corsBuilder.DisallowCredentials();
                    }

                    corsBuilder.WithHeaders(allowedHeaders.ToArray())
                        .WithMethods(allowedMethods.ToArray())
                        .WithOrigins(origins.ToArray())
                        .WithExposedHeaders(exposedHeaders.ToArray());
                });
            });
    }
}
