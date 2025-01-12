namespace ModularMonolith.Modules.Orders.Domain
{
    using System;

    /// <summary>
    /// Represents an attribute that specifies the entity type for a property.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityTypeAttribute<TEntity>() : EntityTypeAttribute(typeof(TEntity)) where TEntity : IDomainEntity
    {
    }

    /// <summary>
    /// Represents an abstract base class for the EntityTypeAttribute.
    /// </summary>
    public abstract class EntityTypeAttribute(Type entityType) : Attribute
    {
        /// <summary>
        /// Gets the entity type.
        /// </summary>
        public Type EntityType => entityType;
    }
}