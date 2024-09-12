namespace Kubis1982.AccessManagement.Security
{
    using Kubis1982.Shared.Security;
    using Microsoft.AspNetCore.Http;
    using System;

    internal class UserContextAccessor(IHttpContextAccessor contextAccessor) : IUserContextAccessor
    {
        private readonly IHttpContextAccessor contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

        public IUserContext Get() => new UserContext(contextAccessor.HttpContext);
    }
}
