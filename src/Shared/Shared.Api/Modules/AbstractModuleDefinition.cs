namespace ModularMonolith.Shared.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an abstract module definition.
    /// </summary>
    public abstract class AbstractModuleDefinition : IEquatable<AbstractModuleDefinition?>
    {
        /// <summary>
        /// Gets the module name.
        /// </summary>
        public virtual string ModuleName => GetType().GetModuleName();

        /// <summary>
        /// Gets the module code.
        /// </summary>
        public abstract string ModuleCode { get; }

        /// <summary>
        /// Adds services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        public void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            OnAddServices(services, configuration);
            services.AddSingleton(this);
            services.AddSingleton(GetType(), this);
            GetType().Assembly.GetTypes()
                    .Where(x => typeof(IModuleEndpoints).IsAssignableFrom(x) && !x.IsAbstract)
                    .OrderBy(x => x.Name)
                    .ToList().ForEach(x =>
                    {
                        services.AddTransient(x);
                        services.AddKeyedTransient(ModuleCode, (provider, key) => (IModuleEndpoints)provider.GetService(x)!);
                    });
        }

        /// <summary>
        /// Called when adding services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        protected virtual void OnAddServices(IServiceCollection services, IConfiguration configuration) { }

        /// <summary>
        /// Uses the module in the application builder.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void UseServices(WebApplication app)
        {
            OnUseServices(app);
        }

        /// <summary>
        /// Called when using the module in the application builder.
        /// </summary>
        /// <param name="app">The application builder.</param>
        protected virtual void OnUseServices(WebApplication app) { }

        /// <summary>
        /// Gets the URL for the specified path.
        /// </summary>
        /// <param name="url">The path.</param>
        /// <returns>The URL.</returns>
        public string GetUrl(string url)
        {
            return $"{ModuleCode.WithLeadingSlash()}{url.WithLeadingSlash()}".ToLowerInvariant();
        }

        /// <summary>
        /// Determines whether the current instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as AbstractModuleDefinition);
        }

        /// <summary>
        /// Determines whether the current instance is equal to the specified module definition.
        /// </summary>
        /// <param name="other">The module definition to compare with the current instance.</param>
        /// <returns>True if the module definitions are equal; otherwise, false.</returns>
        public bool Equals(AbstractModuleDefinition? other)
        {
            return other is not null &&
                   ModuleCode == other.ModuleCode;
        }

        /// <summary>
        /// Gets the hash code for the current instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ModuleCode);
        }

        /// <summary>
        /// Determines whether two module definitions are equal.
        /// </summary>
        /// <param name="left">The first module definition.</param>
        /// <param name="right">The second module definition.</param>
        /// <returns>True if the module definitions are equal; otherwise, false.</returns>
        public static bool operator ==(AbstractModuleDefinition? left, AbstractModuleDefinition? right)
        {
            return EqualityComparer<AbstractModuleDefinition>.Default.Equals(left, right);
        }

        /// <summary>
        /// Determines whether two module definitions are not equal.
        /// </summary>
        /// <param name="left">The first module definition.</param>
        /// <param name="right">The second module definition.</param>
        /// <returns>True if the module definitions are not equal; otherwise, false.</returns>
        public static bool operator !=(AbstractModuleDefinition? left, AbstractModuleDefinition? right)
        {
            return !(left == right);
        }
    }
}
