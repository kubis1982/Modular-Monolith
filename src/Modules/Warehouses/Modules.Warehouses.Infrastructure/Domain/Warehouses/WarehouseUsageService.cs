namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    using System.Collections.Generic;

    /// <summary>
    /// Service for checking if an warehouse is used in any other entity
    /// </summary>
    /// <param name="warehouseUsageCheckers"></param>
    internal class WarehouseUsageService(IEnumerable<IWarehouseUsageChecker> warehouseUsageCheckers) : IWarehouseUsageService
    {
        /// <summary>
        /// Check if an warehouse is used in any other entity
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int warehouseId, out string entityName)
        {
            foreach (IWarehouseUsageChecker checker in warehouseUsageCheckers)
            {
                if (checker.IsUsed(warehouseId, out entityName))
                {
                    return true;
                }
            }
            entityName = string.Empty;
            return false;
        }
    }
}
