namespace ModularMonolith.Modules.ReadModel {
    using System;

    public enum Modules {
        [ModulePrefix("ArM")]
        Articles,
        [ModulePrefix("OrM")]
        Orders,
        [ModulePrefix("WaM")]
        Warehouses,
        [ModulePrefix("CnM")]
        Contractors
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ModulePrefixAttribute(string prefix) : Attribute {
        public string Prefix { get; } = prefix;
    }
}
