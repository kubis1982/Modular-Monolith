namespace ModularMonolith.OpenApi;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using System.Threading;
using System.Threading.Tasks;

internal class OperationODataTransformer : IOpenApiOperationTransformer {
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken) {
        if (context.Description.ActionDescriptor is ControllerActionDescriptor descriptor) {
            if (descriptor.MethodInfo.ReturnType.AssemblyQualifiedName?.StartsWith("System.Threading.Tasks.Task`1[[System.Linq.IQueryable`1") == true) {
                operation.Parameters ??= [];                
                operation.Parameters.Add(new OpenApiParameter() {
                    Name = "$select",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema {
                        Type = "string",
                    },
                    Description = "Returns only the selected properties. (ex. FirstName, LastName, City)",
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter() {
                    Name = "$filter",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema {
                        Type = "string",
                    },
                    Description = "Filter the response with OData filter queries.",
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter() {
                    Name = "$top",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema {
                        Type = "number",
                    },
                    Description = "Number of objects to return. (ex. 25)",
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter() {
                    Name = "$skip",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema {
                        Type = "number",
                    },
                    Description = "Number of objects to skip in the current order (ex. 50)",
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter() {
                    Name = "$orderby",
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema {
                        Type = "string",
                    },
                    Description = "Define the order by one or more fields (ex. LastModified)",
                    Required = false
                });
            }
        }
        return Task.CompletedTask;
    }
}
