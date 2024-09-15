namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users
{
    using ModularMonolith.Modules.AccessManagement.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserRequest {
        /// <summary>
        /// Email of user.
        /// </summary>
        [MaxLength(UserRestriction.EmailLength)]
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password of user.
        /// </summary>
        [Required]
        [MaxLength(UserRestriction.PasswordLength)]
        public string Password { get; set; } = string.Empty;

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
