using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    /// <summary>
    /// Specifies the requirements for elevated state of an application.
    /// </summary>
    public enum ApplicationElevationRequirement
    {
        /// <summary>
        /// Indicates that the <see cref="Application"/> does not require elevation.
        /// </summary>
        ElevationNotRequired = 0,

        /// <summary>
        /// Indicates that the <see cref="Application"/> must be run in an elevated state.
        /// </summary>
        ElevationRequired = 1,

        /// <summary>
        /// Indicates that the <see cref="Application"/> can be run in either an elevated or un-elevated state.
        /// </summary>
        MixedElevation = 2,
    }
}
