using ModularMonolith.Shared;
using ModularMonolith.Shared.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder().Initialize();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddModular(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseModular(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwaggerUI();
}

app.Run();

