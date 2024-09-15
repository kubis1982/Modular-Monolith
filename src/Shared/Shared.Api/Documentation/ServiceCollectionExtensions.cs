namespace ModularMonolith.Shared.Documentation
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.IO;
    using System.Xml.XPath;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEndpointsApiDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds(x =>
                {
                    string moduleName = x.GetModuleName();
                    string name = x.Name;
                    if (name.EndsWith("Model"))
                    {
                        name = name[name.LastIndexOf("Model")] + "Request";
                    }
                    else if (name.EndsWith("Result"))
                    {
                        name = name[name.LastIndexOf("Result")] + "Response";
                    }
                    return string.IsNullOrEmpty(moduleName) ? name : $"{moduleName}: {name}";
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
                    Description = $"{SystemInformation.SystemName}",
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
                swagger.OperationFilter<OperationFilter>();
            });
            return services;
        }
    }
}
