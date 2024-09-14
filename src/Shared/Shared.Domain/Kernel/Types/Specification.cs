namespace ModularMonolith.Shared.Kernel.Types
{
    public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class, IAggregateRoot
    {
        protected Specification()
        {
        }

        object ISpecification<TEntity>.GetInternalSpecification() => InternalSpecification;

        private InternalSpecification<TEntity> InternalSpecification { get; } = new();

        protected ISpecificationBuilder<TEntity> Query => new SpecificationBuilder<TEntity>(InternalSpecification.Query);
    }

    internal class InternalSpecification<TEntity> : Ardalis.Specification.Specification<TEntity>, Ardalis.Specification.ISingleResultSpecification<TEntity>
    {
    }

    internal class SpecificationBuilder<TEntity>(Ardalis.Specification.ISpecificationBuilder<TEntity> specBuilder) : ISpecificationBuilder<TEntity> where TEntity : class
    {
        public object InternalSpecificationBuilder => specBuilder;
    }

    internal class IncludableSpecificationBuilder<TEntity, TProperty>(Ardalis.Specification.IIncludableSpecificationBuilder<TEntity, TProperty> specBuilder) : IIncludableSpecificationBuilder<TEntity, TProperty> where TEntity : class
    {
        public object InternalSpecificationBuilder => specBuilder;
    }

    public interface ISpecificationBuilder<TEntity> where TEntity : class
    {
        object InternalSpecificationBuilder { get; }
    }

    public interface IIncludableSpecificationBuilder<TEntity, TProperty> : ISpecificationBuilder<TEntity> where TEntity : class
    {
    }
}
