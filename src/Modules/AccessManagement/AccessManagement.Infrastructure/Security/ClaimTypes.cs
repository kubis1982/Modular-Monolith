namespace Kubis1982.AccessManagement.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ClaimTypes
    {
        public const string UserId = System.Security.Claims.ClaimTypes.NameIdentifier;
        public const string UserName = System.Security.Claims.ClaimTypes.Name;
    }
}
