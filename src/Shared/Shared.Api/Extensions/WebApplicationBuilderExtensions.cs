namespace ModularMonolith.Shared.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using ModularMonolith.Shared.Modules;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder Initialize(this WebApplicationBuilder builder)
        {
            foreach (var settings in GetSettings("*"))
            {
                builder.Configuration.AddJsonFile(settings);
            }
            foreach (var settings in GetSettings($"*.{builder.Environment.EnvironmentName}"))
            {
                builder.Configuration.AddJsonFile(settings);
            }
            IEnumerable<string> GetSettings(string pattern) => Directory.EnumerateFiles(builder.Environment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
            ModuleLoader.LoadModules(builder.Configuration, $"{SystemInformation.SystemName}.Modules.");
            return builder;
        }
    }
}
