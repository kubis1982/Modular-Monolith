namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using System;

    public sealed record RefreshToken
    {
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpirationDate { get; private set; }

        private RefreshToken()
        {
        }

        private RefreshToken(string token, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new TokenIsEmptyException();
            }
            Token = token;
            ExpirationDate = expirationDate;
        }

        public static RefreshToken Create(string token, DateTime expirationDate) => new(token, expirationDate);
    }
}
