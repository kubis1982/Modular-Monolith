namespace Kubis1982.Shared.Security
{
    using Kubis1982.Shared.Security;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    internal class UserContext : IUserContext
    {
        private readonly int userId;

        public int UserId => userId;

        public string UserName { get; }

        public UserContext(HttpContext? httpContext)
        {
            _ = int.TryParse(httpContext?.User.FindFirstValue(ClaimTypes.UserId), out userId);
            UserName = httpContext?.User.FindFirstValue(ClaimTypes.UserName) ?? string.Empty;
        }
    }
}
