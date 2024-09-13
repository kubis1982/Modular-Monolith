namespace Kubis1982.Shared.Persistance.WriteModel
{
    using Kubis1982.Shared.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    internal class WriteDbContextProvider(IServiceProvider serviceProvider) : IWriteDbContextProvider
    {
        public WriteDbContextBase CreateDbContext(string moduleName)
        {
            Type? dbContextType = AppDomain.CurrentDomain.GetModuleAssemblies(moduleName).SelectMany(n => n.GetTypes()).FirstOrDefault(n => n.IsSubclassOf(typeof(WriteDbContextBase)))
                ?? throw new ArgumentNullException($"Brak modułu ({moduleName})");
            WriteDbContextBase dbContextBase = (WriteDbContextBase)serviceProvider.GetRequiredKeyedService(dbContextType, moduleName);
            return dbContextBase;
        }
    }
}
