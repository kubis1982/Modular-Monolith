namespace ModularMonolith.Modules.Ordering.Domain.Orders
{

    using System.Threading;
    using System.Threading.Tasks;

    public interface IOrderRepository
    {
        /// <summary>
        /// Adds a <see cref="Order"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Order> AddAsync(Order entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="Order"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Order> SingleAsync(OrderSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="Order"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Order?> SingleOrDefaultAsync(OrderSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a <see cref="Order"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Order entity, CancellationToken cancellationToken);
    }
}