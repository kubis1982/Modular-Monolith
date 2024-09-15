using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.CQRS;
using ModularMonolith.Shared.Events;
using ModularMonolith.Shared.Extensions;
using ModularMonolith.Shared.Persistance;
using ModularMonolith.Shared.Serialization;
using ModularMonolith.Shared.Time;
using System;

namespace ModularMonolith.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS();
        services.AddEvents();
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetSystemAssemblies(), includeInternalTypes: true);
        services.AddDatabase(configuration);
        services.AddSingleton<IClock, Clock>();
        services.AddSerialization();
        return services;
    }
}
