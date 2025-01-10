namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits.Events
{
    public record MeasurementUnitCreatedEvent : MeasurementUnitDomainEvent
    {
        public MeasurementUnitCreatedEvent(MeasurementUnit measurementUnit, MeasurementUnitName name, MeasurementUnitCode code) : base(measurementUnit)
        {
            Name = name;
            Code = code;
        }
        public MeasurementUnitName Name { get; }
        public MeasurementUnitCode Code { get; }
    }
}
