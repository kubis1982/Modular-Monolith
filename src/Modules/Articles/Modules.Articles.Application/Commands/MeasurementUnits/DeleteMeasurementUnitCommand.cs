namespace ModularMonolith.Modules.Articles.Commands.MeasurementUnits
{
    using System.Threading;
    using System.Threading.Tasks;

    public record DeleteMeasurementUnitCommand(int MeasurementUnitId) : UnitOfWorkCommand
    {
        internal class DeleteMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository) : UnitOfWorkCommandHandler<DeleteMeasurementUnitCommand>
        {
            public override async Task Handle(DeleteMeasurementUnitCommand request, CancellationToken cancellationToken)
            {
                var measurementUnit = await measurementUnitRepository.SingleAsync(MeasurementUnitSpec.ById(request.MeasurementUnitId), cancellationToken);
                measurementUnit.Remove();
                await measurementUnitRepository.DeleteAsync(measurementUnit, cancellationToken);
            }
        }
    }
}
