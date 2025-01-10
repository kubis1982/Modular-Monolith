using Microsoft.AspNetCore.OpenApi;
using ModularMonolith.OpenApi;
using ModularMonolith.Shared;
using ModularMonolith.Shared.Documentation;
using ModularMonolith.Shared.Extensions;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder().Initialize();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi(n =>
{
    static string GetName(string name)
    {
        if (name.EndsWith("Model"))
        {
            return name[..^5] + "Request";
        }
        if (name.EndsWith("Model[]"))
        {
            return name[..^7] + "Request[]";
        }
        if (name.EndsWith("Result"))
        {
            return name[..^6] + "Response";
        }
        if (name.EndsWith("Result[]"))
        {
            return name[..^8] + "Response[]";
        }
        return name;
    }
    n.AddDocumentTransformer<DocumentTransformer>();
    n.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    n.AddSchemaTransformer<SchemaTransformer>();
    n.AddOperationTransformer<OperationIdAndSummaryTransformer>();
    n.CreateSchemaReferenceId = (type) =>
    {
        string moduleName = type.Type.GetModuleName();
        if (string.IsNullOrEmpty(moduleName))
        {
            return OpenApiOptions.CreateDefaultSchemaReferenceId(type);
        }
        string typeName = GetName(type.Type.Name);

        if (moduleName == "Modules")
            return typeName;

        string? subModuleName = type.Type.Assembly.GetCustomAttributes(true).OfType<SubModuleNameAttribute>().SingleOrDefault()?.GetName(type.Type);
        return !string.IsNullOrWhiteSpace(subModuleName) ? $"{moduleName}{subModuleName}{typeName}" : $"{moduleName}{typeName}";
    };
});

builder.Services.AddModular(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseModular(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(n =>
    {
        n.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        n.WithDotNetFlag(true);
        n.WithTitle("Bootstraper");
    });
}

app.Run();

