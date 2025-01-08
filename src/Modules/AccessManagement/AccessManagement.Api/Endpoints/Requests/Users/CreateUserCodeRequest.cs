using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users
{
    public class CreateUserCodeRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
