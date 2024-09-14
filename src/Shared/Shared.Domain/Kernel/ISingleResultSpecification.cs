namespace ModularMonolith.Shared.Kernel
{
    public interface ISingleResultSpecification<TEntity> : ISpecification<TEntity> where TEntity : class, IAggregateRoot
    {
    }
}
