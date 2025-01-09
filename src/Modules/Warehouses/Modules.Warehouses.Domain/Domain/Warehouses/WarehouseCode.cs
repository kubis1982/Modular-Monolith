namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    using System.Text.RegularExpressions;

    public partial record WarehouseCode
    {
        public const string Pattern = @"[a-zA-Z0-9_-]+";

        public string Value { get; private set; } = string.Empty;

        private WarehouseCode()
        {
        }

        private WarehouseCode(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new IncorrectWarehouseCodeException();
            }
            if (!RegExCodePattern().IsMatch(code))
            {
                throw new CodeIncompatibleRegExRequirementsException(code);
            }
            Value = code ?? string.Empty;
        }

        public static WarehouseCode Of(string? code) => new(code);

        public static implicit operator string(WarehouseCode argument) => argument.Value;

        public static implicit operator WarehouseCode(string code) => Of(code);

        [GeneratedRegex("^" + Pattern + "$")]
        private static partial Regex RegExCodePattern();
    }
}
