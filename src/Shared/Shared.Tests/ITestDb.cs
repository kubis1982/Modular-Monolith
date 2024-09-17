namespace ModularMonolith.Shared
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a test database interface.
    /// </summary>
    public interface ITestDb
    {
        /// <summary>
        /// Adds the specified entities to the test database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entities">The entities to add.</param>
        void Add<TEntity>(params TEntity[] entities) where TEntity : class;

        /// <summary>
        /// Retrieves an array of entities from the test database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="expression">The expression used to filter the entities.</param>
        /// <returns>An array of entities.</returns>
        TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        /// <summary>
        /// Retrieves a single entity from the test database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entity.</param>
        /// <returns>A single entity.</returns>
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        /// <summary>
        /// Retrieves a single entity from the test database based on the specified expression, or null if no entity is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entity.</param>
        /// <returns>A single entity, or null if no entity is found.</returns>
        TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

        /// <summary>
        /// Determines whether any entity exists in the test database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entity.</param>
        /// <returns>True if any entity exists, otherwise false.</returns>
        bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    }
}
