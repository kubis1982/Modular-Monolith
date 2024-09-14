namespace ModularMonolith.Shared.Kernel
{
    public interface ISpecification<TEntity> where TEntity : class, IAggregateRoot
    {
        object GetInternalSpecification();
    }
}
