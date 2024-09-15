namespace ModularMonolith.Shared.Documentation
{
    using Microsoft.OpenApi.Models;
    using ModularMonolith.Shared.Extensions;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Reflection;

    internal class OperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId is null)
            {
                MethodInfo methodInfo = context.MethodInfo;
                string methodName = methodInfo.Name;
                string? moduleName = methodInfo.DeclaringType?.GetModuleName();
                operation.OperationId = string.IsNullOrEmpty(moduleName) ? methodName : $"{moduleName}_{methodName}";
            }
        }
    }
}
