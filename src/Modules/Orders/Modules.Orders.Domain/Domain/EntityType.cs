namespace ModularMonolith.Modules.Orders.Domain
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents an entity type.
    /// </summary>
    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "OrM";

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityType"/> class.
        /// </summary>
        /// <param name="numerator">The numerator value.</param>
        /// <param name="name">The name of the entity type.</param>
        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }

        /// <summary>
        /// Gets the purchase order entity type.
        /// </summary>
        [EntityType<PurchaseOrder>]
        public static EntityType PurchaseOrder => new(1, "Zamówienie zakupu (ZZ - nagłówek)");

        /// <summary>
        /// Gets the purchase order item entity type.
        /// </summary>
        [EntityType<PurchaseOrderItem>]
        public static EntityType PurchaseOrderItem => new(2, "Zamówienie zakupu (ZZ) - pozycja");

        /// <summary>
        /// Gets the sales order entity type.
        /// </summary>
        [EntityType<SalesOrder>]
        public static EntityType SalesOrder => new(3, "Zlecenie sprzedaży (ZS)");

        /// <summary>
        /// Gets the sales order item entity type.
        /// </summary>
        [EntityType<SalesOrderItem>]
        public static EntityType SalesOrderItem => new(4, "Zlecenie sprzedaży (ZS) - pozycja");

        /// <summary>
        /// Gets the prefix for the specified entity type.
        /// </summary>
        /// <param name="entityTypeId">The entity type identifier.</param>
        /// <returns>The prefix for the entity type.</returns>
        public static string GetPrefix(EntityTypeId entityTypeId)
        {
            if (Prefixes.TryGetValue(FromCode<EntityType>(entityTypeId), out string? prefix))
            {
                return prefix;
            }
            throw new Exception($"Prefix for {entityTypeId} not found.");
        }

        /// <summary>
        /// Gets the entity type based on the specified <paramref name="entityType"/>.
        /// </summary>
        /// <param name="entityType">The type of the entity.</param>
        /// <returns>The entity type.</returns>
        public static EntityType GetEntityType(Type entityType)
        {
            var properties = typeof(EntityType).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var property in properties)
            {
                EntityTypeAttribute? attribute = property.GetCustomAttribute<EntityTypeAttribute>();
                if (attribute?.EntityType == entityType)
                {
                    return (EntityType)property.GetValue(null)!;
                }
            }

            throw new InvalidOperationException($"Entity type for {entityType.Name} not defined.");
        }

        private static IDictionary<EntityType, string> Prefixes { get; } = new Dictionary<EntityType, string>()
        {
            { PurchaseOrder, "ZZ" },
            { SalesOrder, "ZS" }
        };
    }
}
