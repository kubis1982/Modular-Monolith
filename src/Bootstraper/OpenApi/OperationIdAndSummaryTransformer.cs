namespace ModularMonolith.OpenApi;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using ModularMonolith.Shared.Documentation;
using ModularMonolith.Shared.Extensions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

internal class OperationIdAndSummaryTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        MethodInfo? methodInfo = context.Description.ActionDescriptor.EndpointMetadata.OfType<MethodInfo>().FirstOrDefault();
        string? methodName = methodInfo?.Name;
        Type? declaringType = methodInfo?.DeclaringType;
        string? moduleName = declaringType?.GetModuleName();

        if (moduleName is not null)
        {
            operation.OperationId ??= string.IsNullOrEmpty(moduleName) ? methodName : $"{moduleName}{methodName}";
            operation.Summary ??= methodName;

            string? subModuleName = declaringType!.Assembly.GetCustomAttributes(true).OfType<SubModuleNameAttribute>().SingleOrDefault()?.GetName(declaringType);

            if (!string.IsNullOrWhiteSpace(subModuleName))
            {
                operation.Tags.Clear();
                operation.Tags.Add(new OpenApiTag { Name = subModuleName });
            }
        }

        return Task.CompletedTask;
    }
}
