namespace ModularMonolith.Shared.Modules
{
    using ModularMonolith.Shared.Extensions;

    public static class ModuleEndpointsExtensions
    {
        public static string GetUrl(this IModuleEndpoints moduleEndpoints, string moduleCode, string path)
        {
            return $"{moduleCode.WithLeadingSlash()}{path.WithLeadingSlash()}".ToLowerInvariant();
        }
    }
}
