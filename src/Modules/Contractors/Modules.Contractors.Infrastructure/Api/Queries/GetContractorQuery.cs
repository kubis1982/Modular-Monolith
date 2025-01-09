namespace ModularMonolith.Modules.Contractors.Api.Queries
{
    using ModularMonolith.Modules.Contractors.Api.Dtos;
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed record GetContractorQuery(int ContractorId) : Query<ContractorDto?>
    {
        internal class GetContractorQueryHandler(ReadDbContext db) : QueryHandler<GetContractorQuery, ContractorDto?>
        {
            public override async Task<ContractorDto?> Handle(GetContractorQuery request, CancellationToken cancellationToken)
            {
                var result = await db.Contractors.Where(x => x.Id == request.ContractorId).Select(x => new ContractorDto
                {
                    TypeId = x.TypeId,
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsBlocked = x.IsBlocked,
                }).SingleOrDefaultAsync(cancellationToken: cancellationToken);

                return result;
            }
        }
    }
}
