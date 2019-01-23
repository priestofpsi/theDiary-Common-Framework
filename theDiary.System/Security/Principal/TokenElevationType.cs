using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Principal
{
    /// <summary>
    /// Indicates the type of elevation being used by a token.
    /// </summary>
    public enum TokenElevationType
    {
        /// <summary>
        /// Indicates that the token does not have a linked token.
        /// </summary>
        TokenElevationTypeDefault = 1,

        /// <summary>
        /// Indicates that the token is an elevated token.
        /// </summary>
        TokenElevationTypeFull,

        /// <summary>
        /// Indicates that the token is a limited token.
        /// </summary>
        TokenElevationTypeLimited
    }
}
