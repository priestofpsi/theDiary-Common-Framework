using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security
{
    public interface IUser
        : IUserSecurity
    {
        string FirstName { get; set; }

        string LastName { get; set; }
    }

    public interface IUserSecurity
    {
        Guid UserIdentifier { get; }

        string EmailAddress { get; set; }

        bool LockedOut { get; }

        bool Verified { get; }
    }
}
