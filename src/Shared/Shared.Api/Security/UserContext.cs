namespace ModularMonolith.Shared.Security
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    internal class UserContext : IUserContext
    {
        private readonly int userId;

        public int Id => userId;

        public string Name { get; }

        public string Email { get; }

        private readonly int sessionId;

        public int SessionId => sessionId;

        public UserContext(HttpContext? httpContext)
        {
            _ = int.TryParse(httpContext?.User.FindFirstValue(ClaimTypes.UserId), out userId);
            Name = httpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            Email = httpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            _ = int.TryParse(httpContext?.User.FindFirstValue(ClaimTypes.SessionId), out sessionId);
        }
    }
}
