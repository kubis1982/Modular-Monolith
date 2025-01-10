namespace ModularMonolith.Modules.Contractors
{
    /// <summary>
    /// Interface for checking if an contractor is used in the system.
    /// </summary>
    public interface IContractorUsageChecker
    {
        /// <summary>
        /// Checks if the contractor is used in the system.
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int contractorId, out string entityName);
    }
}
