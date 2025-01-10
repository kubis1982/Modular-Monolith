namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMeasurementUnitRepository
    {
        /// <summary>
        /// Adds a <see cref="MeasurementUnit"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<MeasurementUnit> AddAsync(MeasurementUnit entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="MeasurementUnit"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<MeasurementUnit> SingleAsync(MeasurementUnitSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a <see cref="MeasurementUnit"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(MeasurementUnit entity, CancellationToken cancellationToken);
    }
}
