namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWarehouseRepository
    {
        /// <summary>
        /// Adds a <see cref="Warehouse"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Warehouse> AddAsync(Warehouse entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="Warehouse"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Warehouse> SingleAsync(WarehouseSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a <see cref="Warehouse"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Warehouse entity, CancellationToken cancellationToken);
    }
}
