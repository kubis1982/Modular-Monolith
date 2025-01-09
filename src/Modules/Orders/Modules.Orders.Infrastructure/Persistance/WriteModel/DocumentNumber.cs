namespace ModularMonolith.Modules.Ordering.Domain
{
    using ModularMonolith.Shared.Kernel.Types;
    using System;

    internal class DocumentNumber
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private DocumentNumber()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public EntityTypeId EntityTypeId { get; private set; }
        public short Year { get; private set; }
        public byte Month { get; private set; }
        public byte Day { get; private set; }
        public short Numerator { get; private set; }

        public static DocumentNumber Create(EntityTypeId entityTypeId, DateTime dateTime, short numerator)
        {
            return new DocumentNumber
            {
                EntityTypeId = entityTypeId,
                Year = Convert.ToInt16(dateTime.Year),
                Month = Convert.ToByte(dateTime.Month),
                Day = Convert.ToByte(dateTime.Day),
                Numerator = numerator
            };
        }

        public void Increase()
        {
            Numerator += 1;
        }

        public override bool Equals(object? obj)
        {
            return obj is DocumentNumber number &&
                   EntityTypeId == number.EntityTypeId &&
                   Year == number.Year &&
                   Month == number.Month &&
                   Day == number.Day;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EntityTypeId, Year, Month, Day);
        }

        public string GetNumber()
        {
            string prefix = EntityType.GetPrefix(EntityTypeId);
            string number = string.Concat(prefix, "-", Year, "/", Month.ToString("D2"), "/", Day.ToString("D2"), "/", Numerator.ToString("D5"));
            return number;
        }
    }
}
