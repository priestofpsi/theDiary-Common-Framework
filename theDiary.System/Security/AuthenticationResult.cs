using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security
{
    public enum AuthenticationResult
    {
        InvalidLoginAndPassword = 0,

        LockedOut = 1,

        WaitingAuthorization = 2,

        Success = 3,
    }
}
