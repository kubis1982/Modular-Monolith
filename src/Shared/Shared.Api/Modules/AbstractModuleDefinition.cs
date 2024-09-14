namespace ModularMonolith.Shared.Modules
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Events;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AbstractModuleDefinition : IEquatable<AbstractModuleDefinition?>
    {
        //private readonly Lazy<EndpointCollection> endpoints;

        public AbstractModuleDefinition()
        {
            //endpoints = new Lazy<EndpointCollection>(() => {
            //    var endpoints = new EndpointCollection(ModuleCode);
            //    OnEndpoints(endpoints);
            //    return endpoints;
            //});
        }

        /// <summary>
        /// 
        /// </summary>
        public string ModuleName => GetType().GetModuleName();

        /// <summary>
        /// 
        /// </summary>
        public abstract string ModuleCode { get; }

        public void AddDependencies(IServiceCollection services, IConfiguration configuration)
        {
            OnDependencies(services, configuration);
            AddSubscribers(services);
        }

        protected abstract void OnDependencies(IServiceCollection services, IConfiguration configuration);

        private void AddSubscribers(IServiceCollection services)
        {
            Type eventCommandType = typeof(EventCommand<>);

            foreach (var assembly in AppDomain.CurrentDomain.GetModuleAssemblies(ModuleName))
            {
                foreach (var type in assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == eventCommandType))
                {
                    services.AddSingleton(new Subscriber(ModuleName, type.GetProperty(nameof(EventCommand<IIntegrationEvent>.Event))!.PropertyType));
                }
            }
        }

        //protected abstract void OnEndpoints(EndpointCollection endpointCollection);

        /// <summary>
        /// Gets the endpoints
        /// </summary>
        /// <returns></returns>
        //public EndpointCollection Endpoints => endpoints.Value;

        public string GetUrl(string url)
        {
            return $"{ModuleCode.WithLeadingSlash()}{url.WithLeadingSlash()}".ToLowerInvariant();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AbstractModuleDefinition);
        }

        public bool Equals(AbstractModuleDefinition? other)
        {
            return other is not null &&
                   ModuleName == other.ModuleName &&
                   ModuleCode == other.ModuleCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ModuleName, ModuleCode);
        }

        public static bool operator ==(AbstractModuleDefinition? left, AbstractModuleDefinition? right)
        {
            return EqualityComparer<AbstractModuleDefinition>.Default.Equals(left, right);
        }

        public static bool operator !=(AbstractModuleDefinition? left, AbstractModuleDefinition? right)
        {
            return !(left == right);
        }
    }
}
