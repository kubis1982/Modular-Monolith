namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits.Events
{
    public record MeasurementUnitUpdatedEvent : MeasurementUnitDomainEvent
    {
        public MeasurementUnitUpdatedEvent(MeasurementUnit measurementUnit, MeasurementUnitName name) : base(measurementUnit)
        {
            Name = name;
        }

        public MeasurementUnitName Name { get; }
    }
}
