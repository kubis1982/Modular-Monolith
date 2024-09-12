namespace Kubis1982.Shared.Security
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using SevenTechnology.Shared;

    public class CorsOptionsSetup(IConfiguration configuration) : IConfigureOptions<CorsOptions>
    {
        private const string KEY = $"{SystemInformation.SystemName}:CORS";
        void IConfigureOptions<CorsOptions>.Configure(CorsOptions options)
        {
            configuration.GetSection(KEY).Bind(options);
        }
    }
}
