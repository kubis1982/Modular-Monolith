namespace ModularMonolith.Modules.Articles.Commands.MeasurementUnits
{
    using ModularMonolith.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateMeasurementUnitCommand(string Code, string Name) : EntityCommand
    {
        internal class CreateMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository) : EntityCommandHandler<CreateMeasurementUnitCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateMeasurementUnitCommand request, CancellationToken cancellationToken)
            {
                MeasurementUnitCode measurementUnitCode = MeasurementUnitCode.Of(request.Code);
                MeasurementUnitName measurementUnitName = MeasurementUnitName.Of(request.Name);
                MeasurementUnit measurementUnit = MeasurementUnit.Create(measurementUnitCode, measurementUnitName);
                await measurementUnitRepository.AddAsync(measurementUnit, cancellationToken);
                return EntityIdentityResult.Create(measurementUnit);
            }
        }
    }
}
