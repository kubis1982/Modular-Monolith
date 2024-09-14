namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using System;

    public sealed record RefreshToken
    {
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiryTime { get; private set; }

        private RefreshToken()
        {
        }

        private RefreshToken(string token, DateTime expiryTime)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new TokenIsEmptyException();
            }
            Token = token;
            ExpiryTime = expiryTime;
        }

        public static RefreshToken Of(string token, DateTime expiryTime) => new(token, expiryTime);
    }
}
