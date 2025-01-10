namespace ModularMonolith.Modules.Contractors.Queries.Addresses
{
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel;
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetAddressesQuery : Query<IEnumerable<GetAddressesQueryResult>>
    {
        private GetAddressesQuery(Expression<Func<AddressEntity, bool>> expression)
        {
            Expression = expression;
        }

        public GetAddressesQuery(int contractorId, int addressId) : this(n => n.ContractorId == contractorId && n.Id == addressId) { }

        public GetAddressesQuery(int contractorId) : this(n => n.ContractorId == contractorId) { }

        public int ContractorId { get; }

        public Expression<Func<AddressEntity, bool>> Expression { get; }

        internal class GetAddressesQueryHandler(ReadDbContext dbContext) : QueryHandler<GetAddressesQuery, IEnumerable<GetAddressesQueryResult>>
        {
            public override async Task<IEnumerable<GetAddressesQueryResult>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Addresses.Where(request.Expression).Select(n => new GetAddressesQueryResult
                {
                    Id = n.Id,
                    Line1 = n.Line1 ?? string.Empty,
                    Line2 = n.Line2 ?? string.Empty,
                    PostalCode = n.PostalCode ?? string.Empty,
                    City = n.City ?? string.Empty,
                    Country = n.Country ?? string.Empty,
                    IsDefault = n.IsDefault
                }).OrderBy(n => n.Id).ToArrayAsync();
            }
        }
    }

    public class GetAddressesQueryResult
    {
        public int Id { get; set; }
        public required string Line1 { get; set; }
        public required string Line2 { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public bool IsDefault { get; set; }
    }
}
