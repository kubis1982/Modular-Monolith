namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IContractorRepository
    {
        /// <summary>
        /// Adds a <see cref="Contractor"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Contractor> AddAsync(Contractor entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="Contractor"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Contractor> SingleAsync(ContractorSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a <see cref="Contractor"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Contractor entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a list of Contractors
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Contractor>> ListAsync(ISpecification<Contractor> spec, CancellationToken cancellationToken);
    }
}
