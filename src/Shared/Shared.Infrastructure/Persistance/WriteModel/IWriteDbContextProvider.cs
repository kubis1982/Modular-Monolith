namespace ModularMonolith.Shared.Persistance.WriteModel
{
    public interface IWriteDbContextProvider
    {
        WriteDbContextBase CreateDbContext(string moduleName);
    }
}
