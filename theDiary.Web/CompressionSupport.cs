using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Compression
{
    /// <summary>
    /// Indicates the compression supported by a web browser.
    /// </summary>
    public enum CompressionSupport
    {
        /// <summary>
        /// No compression is supported.
        /// </summary>
        None,

        /// <summary>
        /// Deflate compression is supported.
        /// </summary>
        Deflate,

        /// <summary>
        /// GZip compression is supported.
        /// </summary>
        GZip,
    }
}
