namespace ModularMonolith.Modules.Articles.Commands.MeasurementUnits
{
    using System.Threading;
    using System.Threading.Tasks;

    public record UpdateMeasurementUnitCommand(int MeasurementUnitId, string Name) : UnitOfWorkCommand
    {
        internal class UpdateMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository) : UnitOfWorkCommandHandler<UpdateMeasurementUnitCommand>
        {
            public override async Task Handle(UpdateMeasurementUnitCommand request, CancellationToken cancellationToken)
            {
                MeasurementUnitId measurementUnitId = new(request.MeasurementUnitId);
                MeasurementUnit measurementUnit = await measurementUnitRepository.SingleAsync(MeasurementUnitSpec.ById(measurementUnitId), cancellationToken);
                MeasurementUnitName measurementUnitName = MeasurementUnitName.Of(request.Name);
                measurementUnit.Update(measurementUnitName);
            }
        }
    }
}
