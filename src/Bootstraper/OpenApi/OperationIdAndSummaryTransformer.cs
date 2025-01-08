namespace ModularMonolith.OpenApi;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using ModularMonolith.Shared.Extensions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

internal class OperationIdAndSummaryTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        if (operation.OperationId is null)
        {
            MethodInfo? methodInfo = context.Description.ActionDescriptor.EndpointMetadata.OfType<MethodInfo>().FirstOrDefault();
            if (methodInfo is not null)
            {
                string methodName = methodInfo.Name;
                string? moduleName = methodInfo.DeclaringType?.GetModuleName();
                operation.OperationId = string.IsNullOrEmpty(moduleName) ? methodName : $"{moduleName}{methodName}";
                operation.Summary = methodName;
            }
        }
        return Task.CompletedTask;
    }
}
