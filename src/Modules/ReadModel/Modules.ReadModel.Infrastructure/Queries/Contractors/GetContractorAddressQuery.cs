namespace ModularMonolith.Modules.ReadModel.Queries.Contractors {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetContractorAddressQuery(int ContractorId, int AddressId) : Query<GetContractorAddressQueryResult?> {
        internal class GetContractorAddressQueryHandler(ReadDbContext dbContext) : QueryHandler<GetContractorAddressQuery, GetContractorAddressQueryResult?> {
            public override async Task<GetContractorAddressQueryResult?> Handle(GetContractorAddressQuery query, CancellationToken cancellationToken) {
                var result = (
                    from address in dbContext.ContractorAddresses.Where(n => n.Id == query.AddressId)
                    select new GetContractorAddressQueryResult {
                        TypeId = address.TypeId,
                        Id = address.Id,
                        IsDefault = address.IsDefault,
                        Address = new AddressResult {
                            Line1 = address.Line1,
                            Line2 = address.Line2,
                            City = address.City,
                            Country = address.Country,
                            PostalCode = address.PostalCode,
                        },
                        Contractor = new EntityResult {
                            TypeId = address.Contractor.TypeId,
                            Id = address.Contractor.Id,
                            Code = address.Contractor.Code,
                            Name = address.Contractor.Name,
                        }
                    }
                );
                return await result.SingleOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class GetContractorAddressQueryResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required bool IsDefault { get; set; }
        public required AddressResult Address { get; set; }
        public required EntityResult Contractor { get; set; }
    }
}