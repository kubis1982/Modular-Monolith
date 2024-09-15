namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users
{
    using ModularMonolith.Modules.AccessManagement.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class UpdateUserRequest
    {
        /// <summary>
        /// First name of user.
        /// </summary>
        [MaxLength(UserRestriction.FirstNameLength)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name of user.
        /// </summary>
        [MaxLength(UserRestriction.LastNameLength)]
        public string? LastName { get; set; }

        /// <summary>
        /// Middle name of user.
        /// </summary>
        [MaxLength(UserRestriction.MiddleNameLength)]
        public string? MiddleName { get; set; }
    }
}
