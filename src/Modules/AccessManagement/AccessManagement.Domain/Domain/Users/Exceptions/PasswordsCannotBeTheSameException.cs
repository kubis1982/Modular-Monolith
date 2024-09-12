namespace Kubis1982.AccessManagement.Domain.Users.Exceptions
{
    using Kubis1982.Shared.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PasswordsCannotBeTheSameException() : AppException("Passwords cannot be the same.")
    {
    }
}
