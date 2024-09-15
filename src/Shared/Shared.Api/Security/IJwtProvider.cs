namespace ModularMonolith.Shared.Security
{
    using System;

    public interface IJwtProvider
    {
        AuthOptions Options { get; }

        string GenerateToken(JwtClaimValues jwtClaimValues, DateTime expiryDate);
    }
}
