namespace ModularMonolith.Modules.Warehouses
{
    /// <summary>
    /// Interface for checking if an warehouse is used in the system.
    /// </summary>
    public interface IWarehouseUsageChecker
    {
        /// <summary>
        /// Checks if the warehouse is used in the system.
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int warehouseId, out string entityName);
    }
}
