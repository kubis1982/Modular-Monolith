namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users
{
    using ModularMonolith.Modules.AccessManagement.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordRequest
    {
        [Required]
        [MaxLength(UserRestriction.PasswordLength)]
        public string Password { get; set; } = string.Empty;
    }
}
