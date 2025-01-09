namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using System.Collections.Generic;

    /// <summary>
    /// Service for checking if an contractor is used in any other entity
    /// </summary>
    /// <param name="contractorUsageCheckers"></param>
    internal class ContractorUsageService(IEnumerable<IContractorUsageChecker> contractorUsageCheckers) : IContractorUsageService
    {
        /// <summary>
        /// Check if an contractor is used in any other entity
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int contractorId, out string entityName)
        {
            foreach (IContractorUsageChecker checker in contractorUsageCheckers)
            {
                if (checker.IsUsed(contractorId, out entityName))
                {
                    return true;
                }
            }
            entityName = string.Empty;
            return false;
        }
    }
}
