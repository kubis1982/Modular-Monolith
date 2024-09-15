namespace ModularMonolith.Shared.Security
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    internal class JwtProvider(IOptionsMonitor<AuthOptions> options) : IJwtProvider
    {
        public AuthOptions Options { get; } = options.CurrentValue;

        public string GenerateToken(JwtClaimValues jwtClaimValues, DateTime expiryDate)
        {
            var issuer = Options.Issuer;
            var audience = Options.Audience;
            var secretKey = Options.SecretKey;

            var key = Encoding.UTF8.GetBytes(secretKey ?? string.Empty);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[] {
                new Claim(ClaimTypes.UserId, jwtClaimValues.UserId.ToString()),
                new Claim(ClaimTypes.Name, jwtClaimValues.UserName),
                new Claim(ClaimTypes.Email, jwtClaimValues.UserEmail),
                new Claim(ClaimTypes.SessionId, jwtClaimValues.SessionId.ToString())
            };

            var token = new JwtSecurityToken(issuer, audience, claims,
                expires: expiryDate,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
