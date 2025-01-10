namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    /// <summary>
    /// Service for checking if an contractor is used in any other entity
    /// </summary>
    public interface IContractorUsageService
    {
        /// <summary>
        /// Check if an contractor is used in any other entity
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int contractorId, out string entityName);
    }
}
