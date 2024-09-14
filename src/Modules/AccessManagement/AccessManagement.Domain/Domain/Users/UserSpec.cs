namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Shared.Kernel;
    using ModularMonolith.Shared.Kernel.Types;
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
        /// Creates a specification to query users by their email.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>The user specification.</returns>
        public static UserSpec ByEmail(UserEmail userEmail)
            => new(n => n.Where(n => n.Email == userEmail));
    }
}
