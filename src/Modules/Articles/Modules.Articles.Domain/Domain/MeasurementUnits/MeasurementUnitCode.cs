namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits.Exceptions;
    using System;
    using System.Text.RegularExpressions;

    public sealed partial record MeasurementUnitCode
    {
        public const string Pattern = "[a-zA-Z0-9\\.]+";

        public string Value { get; private set; } = string.Empty;

        private MeasurementUnitCode()
        {
        }

        private MeasurementUnitCode(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code is empty", nameof(MeasurementUnitCode));
            }
            if (!CodePattern().IsMatch(code))
            {
                throw new MeasurementUnitCodeIncompatiblePatternException(code);
            }

            Value = code ?? string.Empty;
        }

        public static MeasurementUnitCode Of(string? name) => new(name);

        public static implicit operator string(MeasurementUnitCode argument) => argument.Value;

        public static implicit operator MeasurementUnitCode(string name) => Of(name);

        [GeneratedRegex("^" + Pattern + "$")]
        private static partial Regex CodePattern();
    }
}
