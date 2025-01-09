namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    /// <summary>
    /// Service for checking if an warehouse is used in any other entity
    /// </summary>
    public interface IWarehouseUsageService
    {
        /// <summary>
        /// Check if an warehouse is used in any other entity
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int warehouseId, out string entityName);
    }
}
