using Serilog;
using Kubis1982.Shared;
using Kubis1982.Shared.Extensions;

var builder = WebApplication.CreateBuilder();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddModular(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseModular(app.Environment);

app.Run();

