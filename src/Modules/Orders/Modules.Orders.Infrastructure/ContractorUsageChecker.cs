namespace ModularMonolith.Modules.Ordering
{
    using ModularMonolith.Modules.Contractors;
    using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
    using System.Linq;

    /// <summary>
    /// Contractor usage checker
    /// </summary>
    internal class ContractorUsageChecker(ReadDbContext readDbContext) : IContractorUsageChecker
    {
        /// <summary>
        /// Check if an contractor is used in any other entity
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int contractorId, out string entityName)
        {
            if (readDbContext.Orders.Any(n => n.ContractorId == contractorId))
            {
                entityName = "Zamówienie";
                return true;
            }
            entityName = string.Empty;
            return false;
        }
    }
}
