namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    public abstract record MeasurementUnitDomainEvent : IDomainEvent
    {
        private readonly MeasurementUnit measurementUnit;

        public MeasurementUnitDomainEvent(MeasurementUnit measurementUnit)
        {
            this.measurementUnit = measurementUnit;
        }
        public MeasurementUnitId MeasurementUnitId => measurementUnit.Id;
    }
}