using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    /// <summary>
    /// The possible results for the elevation request by an application.
    /// </summary>
    public enum ElevationRequestResult
    {
        /// <summary>
        /// Indicates that the elevation request has not been performed.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Indicates that the request by the <see cref="ElevatedApplication"/> for elevated privileges was refused.
        /// </summary>
        ElevationRefused = 0,

        /// <summary>
        /// Indicates that the request by the <see cref="ElevatedApplication"/> for elevated privileges was granted.
        /// </summary>
        ElevationGranted = 1,

        /// <summary>
        /// Indicates that the current <see cref="Application"/> is currently elevated.
        /// </summary>
        ElevationNotRequired = 2,
    }
}
