namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    using System;

    /// <summary>
    /// Represents an attribute that specifies the order type for a property.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderTypeAttribute<TOrder>() : OrderTypeAttribute(typeof(TOrder)) where TOrder : Order
    {
    }

    /// <summary>
    /// Represents an abstract base class for order type attributes.
    /// </summary>
    public abstract class OrderTypeAttribute(Type orderType) : Attribute
    {
        /// <summary>
        /// Gets the type of the order.
        /// </summary>
        public Type OrderType => orderType;
    }
}