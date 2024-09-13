namespace Kubis1982.Shared.Persistance
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    internal class DbOptionsSetup(IConfiguration configuration) : IConfigureOptions<DbOptions>, IValidateOptions<DbOptions>
    {
        private const string KEY = $"{SystemInformation.SystemName}:Db";

        public void Configure(DbOptions options)
        {
            configuration.GetSection(KEY).Bind(options);
        }

        public ValidateOptionsResult Validate(string? name, DbOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"`{nameof(DbOptions.ConnectionString)}` w `{KEY} nie zostało zdefiniowane.");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
