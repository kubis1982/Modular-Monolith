namespace Kubis1982.Shared.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AppDomainExtensions
    {
        public static Assembly[] GetSystemAssemblies(this AppDomain currentDomain) => currentDomain.GetAssemblies().Where(n => n.FullName?.StartsWith(SystemInformation.SystemName) == true).ToArray();

        public static Assembly[] GetModuleAssemblies(this AppDomain currentDomain, string moduleName)
        {
            return currentDomain.GetSystemAssemblies().Where(n => n.FullName?.StartsWith($"{SystemInformation.SystemName}.Modules.{moduleName}") == true).ToArray();
        }

        public static Assembly? GetModuleSharedAssembly(this AppDomain currentDomain, string moduleName)
        {
            return currentDomain.GetModuleAssemblies(moduleName).FirstOrDefault(n => n.FullName?.StartsWith($"{SystemInformation.SystemName}.Modules.{moduleName}.Shared") == true);
        }
    }
}
