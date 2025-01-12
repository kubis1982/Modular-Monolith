namespace ModularMonolith.Modules.Orders.Domain
{
    using ModularMonolith.Modules.Orders.Domain.Exceptions;

    public record Quantity
    {
        private Quantity(decimal Value, string Unit, int Numerator, int Denominator)
        {
            this.Value = Value;
            this.Unit = Unit;
            this.Numerator = Numerator;
            this.Denominator = Denominator;
        }

        public static Quantity Create(decimal quantity, string unit, int numerator, int denominator)
        {
            if (string.IsNullOrEmpty(unit))
            {
                throw new FieldEmptyException("Jednostka miary", nameof(Quantity));
            }
            if (numerator < 1 || denominator < 1)
            {
                throw new ConverterInvalidValueException();
            }

            return new(quantity, unit, numerator, denominator);
        }

        public decimal Value { get; }
        public string Unit { get; }
        public int Numerator { get; }
        public int Denominator { get; }

        public static implicit operator decimal(Quantity argument) => argument.Value;
    }
}
