namespace ModularMonolith.Shared.Security
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class AuthOptionsSetup(IConfiguration configuration) : IConfigureOptions<AuthOptions>, IValidateOptions<AuthOptions>
    {
        private const string KEY = $"{SystemInformation.SystemName}:Auth";

        public void Configure(AuthOptions options)
        {
            configuration.GetSection(KEY).Bind(options);
        }

        public ValidateOptionsResult Validate(string? name, AuthOptions options)
        {
            string? errorMessage = string.Empty;

            if (string.IsNullOrEmpty(options.SecretKey))
            {
                errorMessage += $"`{nameof(options.SecretKey)}` w `{KEY}` nie został ustawiony\n";
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                errorMessage += $"`{nameof(options.Audience)}` w `{KEY}` nie został ustawiony\n";
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                errorMessage += $"`{nameof(options.Issuer)}` w `{KEY}` nie został ustawiony\n";
            }

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return ValidateOptionsResult.Fail(errorMessage.Trim());
            }

            return ValidateOptionsResult.Success;
        }
    }
}
