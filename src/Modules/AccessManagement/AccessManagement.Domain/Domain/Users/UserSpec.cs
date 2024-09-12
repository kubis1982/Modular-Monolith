namespace Kubis1982.AccessManagement.Domain.Users
{
    using Kubis1982.Shared.Kernel;
    using Kubis1982.Shared.Kernel.Types;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a specification for querying users.
    /// </summary>
    public sealed class UserSpec : Specification<User>, ISingleResultSpecification<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSpec"/> class.
        /// </summary>
        /// <param name="action">The action to build the specification.</param>
        private UserSpec(Action<ISpecificationBuilder<User>> action) => action.Invoke(Query);

        /// <summary>
        /// Creates a specification to query users by their ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The user specification.</returns>
        public static UserSpec ById(UserId userId)
            => new(n => n.Where(n => n.Id == userId));

        /// <summary>
        /// Creates a specification to query users by their name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The user specification.</returns>
        public static UserSpec ByName(UserName userName)
            => new(n => n.Where(n => n.Name == userName));

        /// <summary>
        /// Creates a specification to query users by their email.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>The user specification.</returns>
        public static UserSpec ByEmail(UserEmail userEmail)
            => new(n => n.Where(n => n.Email == userEmail));
    }
}
