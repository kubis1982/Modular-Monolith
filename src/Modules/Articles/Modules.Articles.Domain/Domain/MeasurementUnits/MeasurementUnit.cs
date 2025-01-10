namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{

    public class MeasurementUnit : DomainEntity<MeasurementUnitId, int, EntityType>, IAggregateRoot
    {
        internal MeasurementUnitName Name { get; private set; }
        internal MeasurementUnitCode Code { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private MeasurementUnit()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private MeasurementUnit(MeasurementUnitName name, MeasurementUnitCode code) : this()
        {
            Name = name;
            Code = code;
            AddEvent(new Events.MeasurementUnitCreatedEvent(this, name, code));
        }

        public static MeasurementUnit Create(MeasurementUnitCode code, MeasurementUnitName name)
        {
            return new MeasurementUnit(name, code);
        }

        public void Update(MeasurementUnitName name)
        {
            Name = name;
            AddEvent(new Events.MeasurementUnitUpdatedEvent(this, name));
        }

        public void Remove()
        {
            AddEvent(new Events.MeasurementUnitRemovedEvent(this));
        }
    }
}
