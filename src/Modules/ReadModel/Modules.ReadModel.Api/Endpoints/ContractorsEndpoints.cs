namespace ModularMonolith.Modules.ReadModel.Endpoints
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Modules.ReadModel.Queries.Contractors;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ContractorsEndpoints() : ModuleEndpoints(Modules.Contractors) {
        public override void AddRoutes(IModuleEndpointRouteBuilder endpointRouteBuilder) {
            endpointRouteBuilder.MapGet("contractors/{contractorId}", GetContractor);
            endpointRouteBuilder.MapGet("contractors/{contractorId}/addresses/{addressId}", GetContractorAddress);
        }

        private async Task<Results<Ok<GetContractorQueryResult>, NotFound>> GetContractor([FromServices] IQueryExecutor queryExecutor, [FromRoute] int contractorId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetContractorQuery(contractorId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }

        private async Task<Results<Ok<GetContractorAddressQueryResult>, NotFound>> GetContractorAddress([FromServices] IQueryExecutor queryExecutor, [FromRoute] int contractorId, [FromRoute] int addressId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetContractorAddressQuery(contractorId, addressId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }
    }
}
