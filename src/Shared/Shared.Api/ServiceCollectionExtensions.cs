using Kubis1982.Shared.Extensions;
using Kubis1982.Shared.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Xml.XPath;

namespace Kubis1982.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModular(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();

        static string GetName(string name)
        {
            if (name.EndsWith("Model"))
            {
                return name[..name.LastIndexOf("Model")] + "Request";
            }
            if (name.EndsWith("Result"))
            {
                return name[..name.LastIndexOf("Result")] + "Response";
            }
            return name;
        }
        services.AddSwaggerGen(swagger => {
            swagger.CustomSchemaIds(x => {
                string moduleName = x.GetModuleName();
                if (string.IsNullOrEmpty(moduleName))
                    return GetName(x.Name);
                return $"{moduleName}: {GetName(x.Name)}";
            });
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular API",
                Version = "v1"
            });
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Kubis1982",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            swagger.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"{SystemInformation.SystemName}.*.xml");
            foreach (var file in files)
            {
                var xmlDoc = new XPathDocument(file);
                swagger.IncludeXmlComments(() => xmlDoc);

            }
            swagger.EnableAnnotations();
        });
        services.AddHttpContextAccessor();
        services.AddSecurity(configuration);
        services.AddSharedInfrastructure(configuration);
        return services;
    }
}
