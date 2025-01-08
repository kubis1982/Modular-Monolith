namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using ModularMonolith.Modules.AccessManagement.Services;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Security;
    using System;
    using System.Text;

    internal class ModuleDefinition : AbstractModuleDefinition
    {
        public const string MODULE_CODE = ServiceCollectionExtensions.ModuleCode;

        public override string ModuleCode => MODULE_CODE;

        public override string ModuleName => "Access Management";

        protected override void OnAddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextAccessor, UserContextAccessor>();
            services.AddScoped(n => n.GetRequiredService<IUserContextAccessor>().Get());
            services.ConfigureOptions<AuthOptionsSetup>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                AuthOptions authOptions = services.BuildServiceProvider().GetRequiredService<IOptionsMonitor<AuthOptions>>().CurrentValue;
                var key = Encoding.UTF8.GetBytes(authOptions.SecretKey ?? string.Empty);
                var signingKey = new SymmetricSecurityKey(key);
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();
            services.AddModularInfrastructure(configuration);
        }

        protected override void OnUseServices(IApplicationBuilder app)
        {
            base.OnUseServices(app);

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
