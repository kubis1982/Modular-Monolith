namespace ModularMonolith.Modules.Contractors.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.Contractors.Commands.Contractors;
    using ModularMonolith.Modules.Contractors.Queries.Addresses;
    using ModularMonolith.Modules.Contractors.Requests;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ContractorEndpoints : IModuleEndpoints
    {
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("contractors", CreateContractor);
            endpointRouteBuilder.MapPut("contractors/{contractorId}", UpdateContractor);
            endpointRouteBuilder.MapDelete("contractors/{contractorId}", DeleteContractor);
            endpointRouteBuilder.MapPatch("contractors/{contractorId}/block", BlockContractor);
            endpointRouteBuilder.MapPatch("contractors/{contractorId}/unblock", UnblockContractor);
            endpointRouteBuilder.MapGet("contractors/{contractorId}/addresses", GetAddresses);
            endpointRouteBuilder.MapGet("contractors/{contractorId}/addresses/{addressId}", GetAddress);

            endpointRouteBuilder.MapPost("contractors/{contractorId}/addresses", CreateAddress);
            endpointRouteBuilder.MapDelete("contractors/{contractorId}/addresses/{addressId}", DeleteAddress);
            endpointRouteBuilder.MapPut("contractors/{contractorId}/addresses/{addressId}", UpdateAddress);
            endpointRouteBuilder.MapPatch("contractors/{contractorId}/addresses/{addressId}/default", SetDefaultAddress);
        }

        /// <summary>
        /// Creates a contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreateContractor([FromServices] ICommandExecutor commandExecutor, [FromBody] CreateContractorRequest request, CancellationToken cancellationToken)
        {
            var contractor = await commandExecutor.Execute(new CreateContractorCommand
            {
                Code = request.Code,
                Name = request.Name,
                Country = request.Country,
                Description = request.Description,
                AddressLine1 = request.Address.Line1,
                AddressLine2 = request.Address.Line2,
                AddressPostalCode = request.Address.PostalCode,
                AddressCity = request.Address.City,
                AddressCountry = request.Address.Country,
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"/contractors/{contractor.Id}"), (IdentityResponse)contractor);
        }

        /// <summary>
        /// Updates a contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdateContractor([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, [FromBody] UpdateContractorRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateContractorCommand(contractorId)
            {
                AddressId = request.Address.Id,
                Code = request.Code,
                Name = request.Name,
                Country = request.Country,
                Description = request.Description,
                AddressLine1 = request.Address.Line1,
                AddressLine2 = request.Address.Line2,
                AddressPostalCode = request.Address.PostalCode,
                AddressCity = request.Address.City,
                AddressCountry = request.Address.Country,
            }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeleteContractor([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteContractorCommand(contractorId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Blocks a contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> BlockContractor([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new BlockContractorCommand(contractorId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Unblocks a contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UnblockContractor([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UnblockContractorCommand(contractorId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Adds new address to contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreateAddress([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, [FromBody] CreateAddressRequest request, CancellationToken cancellationToken)
        {
            var address = await commandExecutor.Execute(new CreateAddressCommand(contractorId)
            {
                AddressLine1 = request.Line1,
                AddressLine2 = request.Line2,
                AddressPostalCode = request.PostalCode,
                AddressCity = request.City,
                AddressCountry = request.Country,
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"/contractors/{contractorId}/addresses/{address.Id}"), (IdentityResponse)address);
        }

        /// <summary>
        /// Deletes address from contractor
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="addressId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeleteAddress([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, [FromRoute] int addressId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteAddressCommand(contractorId, addressId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Updates address
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="addressId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdateAddress([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, [FromRoute] int addressId, [FromBody] UpdateAddressRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateAddressCommand(contractorId, addressId)
            {
                AddressLine1 = request.Line1,
                AddressLine2 = request.Line2,
                AddressPostalCode = request.PostalCode,
                AddressCity = request.City,
                AddressCountry = request.Country,
            }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Set default address
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="contractorId"></param>
        /// <param name="addressId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> SetDefaultAddress([FromServices] ICommandExecutor commandExecutor, [FromRoute] int contractorId, [FromRoute] int addressId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new SetDefaultAddressCommand(contractorId, addressId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Gets all contractor addresses
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="queryExecutor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok<IEnumerable<GetAddressesQueryResult>>> GetAddresses([FromServices] IQueryExecutor queryExecutor, [FromRoute] int contractorId, CancellationToken cancellationToken)
        {
            var results = await queryExecutor.Execute(new GetAddressesQuery(contractorId), cancellationToken);
            return TypedResults.Ok(results);
        }

        /// <summary>
        /// Gets contractor address
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="addressId"></param>
        /// <param name="queryExecutor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Results<Ok<GetAddressesQueryResult>, NotFound>> GetAddress([FromServices] IQueryExecutor queryExecutor, [FromRoute] int contractorId, [FromRoute] int addressId, CancellationToken cancellationToken)
        {
            var result = (await queryExecutor.Execute(new GetAddressesQuery(contractorId, addressId), cancellationToken)).FirstOrDefault();
            return result switch
            {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result),
            };
        }
    }
}
