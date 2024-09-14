namespace ModularMonolith.Shared.Persistance.WriteModel
{
    using ModularMonolith.Shared.Exceptions;
    using ModularMonolith.Shared.Kernel;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class RepositoryInternal<TEntity>(DbContext dbContext) : Ardalis.Specification.EntityFrameworkCore.RepositoryBase<TEntity>(dbContext) where TEntity : class, IAggregateRoot
    {
    }

    public class Repository<TEntity>(DbContext dbContext) where TEntity : class, IAggregateRoot
    {
        private protected readonly RepositoryInternal<TEntity> repository = new(dbContext);

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken) => repository.AddAsync(entity, cancellationToken);

        public Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => repository.AddRangeAsync(entities, cancellationToken);

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken) => repository.DeleteAsync(entity, cancellationToken);

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => repository.DeleteRangeAsync(entities, cancellationToken);

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken) => repository.UpdateAsync(entity, cancellationToken);

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => repository.UpdateRangeAsync(entities, cancellationToken);

        public Task<TEntity?> SingleOrDefaultAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : notnull => repository.GetByIdAsync(key, cancellationToken);

        public async Task<TEntity> SingleAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : notnull => await repository.GetByIdAsync(key, cancellationToken) ?? throw CreateEntityNotFoundException();

        public Task<TEntity?> SingleOrDefaultAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken) => repository.SingleOrDefaultAsync((Ardalis.Specification.ISingleResultSpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        public async Task<TEntity> SingleAsync(ISingleResultSpecification<TEntity> spec, CancellationToken cancellationToken) => await repository.SingleOrDefaultAsync((Ardalis.Specification.ISingleResultSpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken) ?? throw CreateEntityNotFoundException();

        public Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken) => repository.FirstOrDefaultAsync((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        public async Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken) => await repository.FirstOrDefaultAsync((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken) ?? throw CreateEntityNotFoundException();

        public Task<bool> AnyAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken) => repository.AnyAsync((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        public Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken) => repository.CountAsync((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        public IAsyncEnumerable<TEntity> AsAsyncEnumerable(ISpecification<TEntity> spec) => repository.AsAsyncEnumerable((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification());

        public Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken) => repository.ListAsync((Ardalis.Specification.ISpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        protected virtual EntityNotFoundException CreateEntityNotFoundException() => new(typeof(TEntity).Name);
    }

    public class Repository<TEntity, TSpecification>(DbContext dbContext) : Repository<TEntity>(dbContext) where TEntity : class, IAggregateRoot where TSpecification : ISingleResultSpecification<TEntity>
    {
        public Task<TEntity?> SingleOrDefaultAsync(TSpecification spec, CancellationToken cancellationToken) => repository.SingleOrDefaultAsync((Ardalis.Specification.ISingleResultSpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken);

        public async Task<TEntity> SingleAsync(TSpecification spec, CancellationToken cancellationToken) => await repository.SingleOrDefaultAsync((Ardalis.Specification.ISingleResultSpecification<TEntity>)spec.GetInternalSpecification(), cancellationToken) ?? throw CreateEntityNotFoundException();
    }
}
