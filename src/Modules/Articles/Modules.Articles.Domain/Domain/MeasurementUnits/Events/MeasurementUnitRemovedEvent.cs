namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits.Events
{
    public record MeasurementUnitRemovedEvent : MeasurementUnitDomainEvent
    {
        public MeasurementUnitRemovedEvent(MeasurementUnit measurementUnit) : base(measurementUnit)
        {
        }
    }
}
