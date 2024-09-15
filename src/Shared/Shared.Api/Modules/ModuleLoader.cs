namespace ModularMonolith.Shared.Modules
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal static class ModuleLoader
    {
        public static void LoadModules(IConfiguration configuration, string modulePart)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }

            files = files.Where(n => n.Contains(SystemInformation.SystemName)).ToList();

            files.ForEach(x =>
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(x);

                try
                {
                    Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                {
                }
            });            
        }
    }
}
