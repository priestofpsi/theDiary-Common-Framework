using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Specifies the even that a file is performing.
    /// </summary>
    public enum FileEvent
    {
        /// <summary>
        /// The file is been Read.
        /// </summary>
        Read,

        /// <summary>
        /// The file is been Saved.
        /// </summary>
        Save,

        /// <summary>
        /// The file is been Deleted.
        /// </summary>
        Delete,
    }
}
