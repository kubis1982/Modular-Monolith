namespace ModularMonolith.Modules.Articles.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.Articles.Commands.MeasurementUnits;
    using ModularMonolith.Modules.Articles.Queries.MeasurementUnits;
    using ModularMonolith.Modules.Articles.Requests.Articles;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MeasurementUnitEndpoints : IModuleEndpoints
    {
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var measurementUnitsGroup = endpointRouteBuilder.MapGroup("measurement-units");

            measurementUnitsGroup.MapPost("", CreateMeasurementUnit);
            measurementUnitsGroup.MapPut("{measurementUnitId}", UpdateMeasurementUnit);
            measurementUnitsGroup.MapDelete("{measurementUnitId}", DeleteMeasurementUnit);
            measurementUnitsGroup.MapGet("", GetMeasurementUnits);
            measurementUnitsGroup.MapGet("{measurementUnitId:int}", GetMeasurementUnit);
        }

        /// <summary>
        /// Creates a measurement unit
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreateMeasurementUnit([FromServices] ICommandExecutor commandExecutor, [FromBody] CreateMeasurementUnitRequest request, CancellationToken cancellationToken)
        {
            var measurementUnit = await commandExecutor.Execute(new CreateMeasurementUnitCommand(request.Code, request.Name), cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"measurement-units/{measurementUnit.Id}"), (IdentityResponse)measurementUnit);
        }

        /// <summary>
        /// Updates a measurement unit
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="measurementUnitId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdateMeasurementUnit([FromServices] ICommandExecutor commandExecutor, [FromRoute] int measurementUnitId, [FromBody] UpdateMeasurementUnitRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateMeasurementUnitCommand(measurementUnitId, request.Name), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a measurement unit
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="measurementUnitId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeleteMeasurementUnit([FromServices] ICommandExecutor commandExecutor, [FromRoute] int measurementUnitId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteMeasurementUnitCommand(measurementUnitId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Gets a measurement units
        /// </summary>
        /// <param name="queryExecutor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok<IEnumerable<GetMeasurementUnitsQueryResult>>> GetMeasurementUnits([FromServices] IQueryExecutor queryExecutor, CancellationToken cancellationToken)
        {
            var results = await queryExecutor.Execute(new GetMeasurementUnitsQuery(), cancellationToken);
            return TypedResults.Ok(results);
        }

        /// <summary>
        /// Gets a measurement unit
        /// </summary>
        /// <param name="queryExecutor"></param>
        /// <param name="measurementUnitId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Results<Ok<GetMeasurementUnitsQueryResult>, NotFound>> GetMeasurementUnit([FromServices] IQueryExecutor queryExecutor, [FromRoute] int measurementUnitId, CancellationToken cancellationToken)
        {
            var result = (await queryExecutor.Execute(new GetMeasurementUnitsQuery(measurementUnitId), cancellationToken)).FirstOrDefault();
            return result switch
            {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };

        }
    }
}
