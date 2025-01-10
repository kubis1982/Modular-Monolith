namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{

    public record MeasurementUnitId : EntityId<int, EntityType>
    {
        public MeasurementUnitId() : this(0)
        {

        }

        public MeasurementUnitId(int id) : base(EntityType.MeasurementUnit, id)
        {
        }

        public static MeasurementUnitId Kg => new(1);

        public static implicit operator MeasurementUnitId(int value) => new(value);
    }
}
