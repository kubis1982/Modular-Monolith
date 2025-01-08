namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    using System;

    public record MeasurementUnitName
    {
        public string Value { get; private set; } = string.Empty;

        private MeasurementUnitName()
        {
        }

        private MeasurementUnitName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Unit name is empty", nameof(MeasurementUnitName));
            }
            Value = name ?? string.Empty;
        }

        public static MeasurementUnitName Of(string? name) => new(name);

        public static implicit operator string(MeasurementUnitName argument) => argument.Value;

        public static implicit operator MeasurementUnitName(string name) => Of(name);
    }
}
