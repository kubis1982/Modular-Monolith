namespace ModularMonolith.Shared.Security
{
    using Microsoft.AspNetCore.Http;
    using System;

    internal class UserContextAccessor(IHttpContextAccessor contextAccessor) : IUserContextAccessor
    {
        private readonly IHttpContextAccessor contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

        public IUserContext Get() => new UserContext(contextAccessor.HttpContext);
    }
}
