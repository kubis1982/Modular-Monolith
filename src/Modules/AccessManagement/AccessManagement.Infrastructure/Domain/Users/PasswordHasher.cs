namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Shared.Security.Cryptography;
    using System.Security.Cryptography;

    internal class PasswordHasher : HashCryptography, IPasswordHasher
    {
        public PasswordHasher() : base(SHA256.Create())
        {
        }
    }
}
