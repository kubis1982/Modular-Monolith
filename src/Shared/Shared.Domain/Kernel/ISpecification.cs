namespace Kubis1982.Shared.Kernel
{
    public interface ISpecification<TEntity> where TEntity : class, IAggregateRoot
    {
        object GetInternalSpecification();
    }
}
