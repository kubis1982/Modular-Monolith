namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Sessions
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSessionRequest
    {
        /// <summary>
        /// Name of user.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Password of user.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
