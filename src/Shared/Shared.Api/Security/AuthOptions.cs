namespace ModularMonolith.Shared.Security
{
    public class AuthOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
        public int TokenValidityInMinutes { get; set; } = 60;
        public int RefreshTokenValidityInDays { get; set; } = 30;
    }
}
