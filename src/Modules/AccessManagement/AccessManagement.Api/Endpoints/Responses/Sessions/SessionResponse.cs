namespace ModularMonolith.Modules.AccessManagement.Endpoints.Responses.Sessions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionResponse
    {
        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string RefreshToken { get; set; }

        [Required]
        public required DateTime ExpiryDate { get; set; }

        [Required]
        public required DateTime RefreshTokenExpiryDate { get; set; }
    }
}
