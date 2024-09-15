namespace ModularMonolith.Shared.Modules
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Extensions;
    using System;
    using System.Collections.Generic;

    public abstract class AbstractModuleDefinition : IEquatable<AbstractModuleDefinition?>
    {
        public virtual string ModuleName => GetType().GetModuleName();

        public abstract string ModuleCode { get; }

        public virtual void AddDependencies(IServiceCollection services, IConfiguration configuration) { }

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
                   ModuleCode == other.ModuleCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ModuleCode);
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
