namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    using ModularMonolith.Modules.Orders.Domain.Orders.Exceptions;

    public record OrderNumber
    {
        public string Value { get; private set; } = string.Empty;

        private OrderNumber()
        {
        }

        private OrderNumber(string? documentNo)
        {
            if (string.IsNullOrWhiteSpace(documentNo))
            {
                throw new OrderNumberEmptyException(nameof(OrderNumber));
            }
            Value = documentNo ?? string.Empty;
        }

        public static OrderNumber Of(string? documentNo) => new(documentNo);

        public static implicit operator string(OrderNumber argument) => argument.Value;
        public static implicit operator OrderNumber(string argument) => Of(argument);
    }
}
