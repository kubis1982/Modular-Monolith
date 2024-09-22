namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using System;

    public sealed record UserToken
    {
        public Guid Token { get; private set; }

        public DateTime ExpirationDate { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private UserToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public static UserToken Create(DateTime expirationDate)
        {
            return new UserToken { Token = Guid.NewGuid(), ExpirationDate = expirationDate };
        }
    }
}
