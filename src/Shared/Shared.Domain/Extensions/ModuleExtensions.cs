namespace ModularMonolith.Shared.Extensions
{
    using System;

    public static class ModuleExtensions
    {
        /// <summary>
        /// Gets a module name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetModuleName(this object value)
            => value?.GetType().GetModuleName() ?? string.Empty;

        /// <summary>
        /// Gets a module name.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetModuleName(this Type type)
        {
            string namespacePart = "Modules";
            int splitIndex = 2;

            if (type?.Namespace is null)
            {
                return string.Empty;
            }
            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex]
                : string.Empty;
        }
    }
}
