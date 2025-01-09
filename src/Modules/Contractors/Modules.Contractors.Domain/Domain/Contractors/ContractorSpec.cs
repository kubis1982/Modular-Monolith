namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using System;
    using System.Linq;

    public sealed class ContractorSpec : Specification<Contractor>, ISingleResultSpecification<Contractor>
    {
        private ContractorSpec(Action<ISpecificationBuilder<Contractor>> action)
          => action(Query);

        public static ContractorSpec ById(ContractorId contractorId)
            => new(n => n.Where(n => n.Id == contractorId));

        public static ContractorSpec ByIds(ContractorId[] contractorsIds)
            => new(query => query.Where(n => contractorsIds.Contains(n.Id)));

        public static ContractorSpec ByIdWithAddress(ContractorId contractorId, ContractorAddressId addressId)
            => new(n => n.Where(n => n.Id == contractorId).Include(n => n.Addresses.Where(m => m.Id == addressId)));

        public static ContractorSpec ByIdWithAddresses(ContractorId contractorId)
            => new(n => n.Where(n => n.Id == contractorId).Include(n => n.Addresses));
    }
}
