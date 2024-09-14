namespace ModularMonolith.ArchitectureTests
{
    using ModularMonolith.Shared;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class AssemblyHelper {
        static AssemblyHelper() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .Where(x => x.Contains(SystemInformation.SystemName))
                .ToList();

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));
        }

        public static Assembly[] GetModuleAssemblies() => GetAssemblies().Where(n => n.FullName?.Contains($"Modules") == true).ToArray();
        public static Assembly[] GetDomainAssemblies() => GetModuleAssemblies().Where(n => n.FullName?.Contains("Domain") == true).ToArray();
        public static Assembly[] GetApplicationAssemblies() => GetModuleAssemblies().Where(n => n.FullName?.Contains("Application") == true).ToArray();
        public static Assembly[] GetInfrastructureAssemblies() => GetModuleAssemblies().Where(n => n.FullName?.Contains("Infrastructure") == true).ToArray();
        public static Assembly[] GetApiAssemblies() => GetModuleAssemblies().Where(n => n.FullName?.Contains("Api") == true).ToArray();
        public static Assembly[] GetSharedAssemblies() => GetAssemblies().Where(n => n.FullName?.StartsWith($"{SystemInformation.SystemName}.Shared") == true).ToArray();
        public static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetSystemAssemblies();
    }
}
