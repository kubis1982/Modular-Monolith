namespace ModularMonolith.Shared
{
    /// <summary>
    /// Represents the interface for the test database context.
    /// </summary>
    public interface ITestDbContext : ITestDb, IDisposable
    {
        /// <summary>
        /// Saves the changes made in the context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();
    }
}
