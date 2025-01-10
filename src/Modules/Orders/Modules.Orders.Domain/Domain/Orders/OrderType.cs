namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents an order type.
    /// </summary>
    public class OrderType : Enumeration<byte>
    {
        private OrderType(byte id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Represents a purchase order.
        /// </summary>
        [OrderType<PurchaseOrder>]
        public static OrderType Purchase => new(1, "Zakup");

        /// <summary>
        /// Represents a sales order.
        /// </summary>
        [OrderType<SalesOrder>]
        public static OrderType Sale => new(2, "Sprzedaż");

        /// <summary>
        /// Gets the order type based on the specified order type.
        /// </summary>
        /// <param name="orderType">The order type.</param>
        /// <returns>The order type.</returns>
        public static OrderType GetOrderType(Type orderType)
        {
            foreach (var item in typeof(OrderType).GetProperties(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly))
            {
                OrderTypeAttribute? attribute = item.GetCustomAttribute<OrderTypeAttribute>();
                if (attribute?.OrderType == orderType)
                {
                    return (OrderType)item.GetValue(null)!;
                }
            }

            throw new InvalidOperationException($"Order type for {orderType.Name} not defined.");
        }
    }
}
