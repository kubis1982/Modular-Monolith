namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> AddAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="userSpec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> SingleAsync(UserSpec userSpec, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(User user, CancellationToken cancellationToken);
    }
}
