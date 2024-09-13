namespace Kubis1982.Shared.Persistance.WriteModel
{
    public interface IWriteDbContextProvider
    {
        WriteDbContextBase CreateDbContext(string moduleName);
    }
}
