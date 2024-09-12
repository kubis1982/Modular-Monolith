namespace Kubis1982.AccessManagement.Domain.Users
{
    /// <summary>
    /// Represents an interface for password hashing.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Computes the hash value for the specified password.
        /// </summary>
        /// <param name="password">The password to compute the hash value for.</param>
        /// <returns>The computed hash value as a string.</returns>
        string Compute(string? password);
    }
}
