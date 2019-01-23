using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Hosting
{
    /// <summary>
    /// Represents a file object from an Embedded Resource.
    /// </summary>
    public sealed class EmbeddedVirtualFile 
        : VirtualFile
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedVirtualFile"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path represented by this instance.</param>
        /// <param name="stream">The <see cref="Stream"/> representing the virtual path.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="stream"/> is <c>Null</c>.</exception>
        public EmbeddedVirtualFile(string virtualPath, Stream stream)
            : base(virtualPath)
        {
            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            this.stream = stream;
        }
        #endregion

        #region Private Declarations
        private Stream stream;
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Returns a read-only stream to the embedded resourceresource.
        /// </summary>
        /// <returns>A read-only stream to the embedded resource.</returns>
        public override Stream Open()
        {
            return this.stream;
        }
        #endregion
    }
}
