namespace Kubis1982.Shared.Kernel
{
    public interface ISingleResultSpecification<TEntity> : ISpecification<TEntity> where TEntity : class, IAggregateRoot
    {
    }
}
