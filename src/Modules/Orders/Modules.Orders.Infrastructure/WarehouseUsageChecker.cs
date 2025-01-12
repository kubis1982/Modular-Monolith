namespace ModularMonolith.Modules.Orders
{
    using ModularMonolith.Modules.Orders.Persistance.ReadModel;
    using ModularMonolith.Modules.Warehouses;
    using System.Linq;

    /// <summary>
    /// Warehouse usage checker
    /// </summary>
    internal class WarehouseUsageChecker(ReadDbContext readDbContext) : IWarehouseUsageChecker
    {
        /// <summary>
        /// Check if an warehouse is used in any other entity
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int warehouseId, out string entityName)
        {
            if (readDbContext.Orders.Any(n => n.WarehouseId == warehouseId))
            {
                entityName = "Zamówienie";
                return true;
            }
            entityName = string.Empty;
            return false;
        }
    }
}
