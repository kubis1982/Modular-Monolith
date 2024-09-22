namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users
{
    using ModularMonolith.Modules.AccessManagement.Persistance;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordByTokenRequest
    {
        [Required]
        public Guid Token { get; set; }

        [Required]
        [MaxLength(UserRestriction.PasswordLength)]
        public string Password { get; set; } = string.Empty;
    }
}
