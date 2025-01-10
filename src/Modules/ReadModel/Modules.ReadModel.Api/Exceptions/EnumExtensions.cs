namespace ModularMonolith.Modules.ReadModel.Exceptions {
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class EnumExtensions {
        public static string GetModulePrefixFromEnumValue(this Enum value) {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            ModulePrefixAttribute? prefixAttribute = field?.GetCustomAttributes(typeof(ModulePrefixAttribute), false).SingleOrDefault() as ModulePrefixAttribute;
            return prefixAttribute?.Prefix?.ToLower() ?? throw new ArgumentException($"Brak prefixu dla modułu: {field?.Name}");
        }
    }
}
