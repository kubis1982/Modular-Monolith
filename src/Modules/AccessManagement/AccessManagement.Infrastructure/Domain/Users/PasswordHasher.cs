namespace Kubis1982.Modules.AccessManagement.Domain.Users
{
    using Kubis1982.Shared.Security.Cryptography;
    using System.Security.Cryptography;

    public class PasswordHasher : HashCryptography, IPasswordHasher
    {
        public PasswordHasher() : base(SHA256.Create())
        {
        }
    }
}
