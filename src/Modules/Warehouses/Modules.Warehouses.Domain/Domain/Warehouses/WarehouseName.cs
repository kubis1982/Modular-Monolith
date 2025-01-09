namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    public record WarehouseName
    {
        public string Value { get; private set; } = string.Empty;

        private WarehouseName()
        {
        }

        private WarehouseName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IncorrectWarehouseNameException();
            }
            Value = name ?? string.Empty;
        }

        public static WarehouseName Of(string? name) => new(name);

        public static implicit operator string(WarehouseName argument) => argument.Value;

        public static implicit operator WarehouseName(string name) => Of(name);
    }
}
