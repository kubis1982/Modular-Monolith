namespace ModularMonolith.Modules.ReadModel.Queries.Contractors {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetContractorQuery(int ContractorId) : Query<GetContractorQueryResult?> {
        internal class GetContractorQueryHandler(ReadDbContext dbContext) : QueryHandler<GetContractorQuery, GetContractorQueryResult?> {
            public override async Task<GetContractorQueryResult?> Handle(GetContractorQuery query, CancellationToken cancellationToken) {
                var result = (
                    from contractors in dbContext.Contractors.Where(n => n.Id == query.ContractorId)
                    join addresses in dbContext.ContractorAddresses.Where(n => n.IsDefault) on contractors.Id equals addresses.Contractor.Id into addressesTemp
                    from addresses in addressesTemp.DefaultIfEmpty()
                    select new GetContractorQueryResult {
                        TypeId = contractors.TypeId,
                        Id = contractors.Id,
                        Code = contractors.Code,
                        Name = contractors.Name,
                        Description = contractors.Description,
                        Country = contractors.Country ?? "PL",
                        Address = addresses != null ? new ContractorAddressResult {
                            TypeId = addresses.TypeId,
                            Id = addresses.Id,
                            Line1 = addresses.Line1,
                            Line2 = addresses.Line2,
                            PostalCode = addresses.PostalCode,
                            City = addresses.City,
                            Country = addresses.Country
                        } : null,
                        IsBlocked = contractors.IsBlocked
                    }
                );
                return await result.FirstOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class ContractorAddressResult : AddressResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
    }

    public class GetContractorQueryResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required string Country { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
        public ContractorAddressResult? Address { get; set; }
        public required bool IsBlocked { get; set; }
        
    }
}