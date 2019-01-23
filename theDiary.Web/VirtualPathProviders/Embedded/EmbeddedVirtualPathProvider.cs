using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Hosting;

namespace System.Web.Hosting
{
    /// <summary>
    /// Provides a set of methods that enable a Web application to retrieve resources from an embedded resource.
    /// </summary>
    public class EmbeddedVirtualPathProvider
        : VirtualPathProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedVirtualPathProvider"/> class.
        /// </summary>
        /// <param name="embeddedAssembly">The <see cref="Assembly"/> instance containing the embedded resources.</param>
        public EmbeddedVirtualPathProvider(Assembly embeddedAssembly)
            : base()
        {
            if (embeddedAssembly.IsNull())
                throw new ArgumentNullException("embeddedAssembly");

            this.embeddedAssembly = embeddedAssembly;
        }
        #endregion

        #region Private Declarations
        private Assembly embeddedAssembly;
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <param name="virtualPath">The path to the primary virtual resource.</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
        /// <returns>A <see cref="System.Web.Caching.CacheDependency"/> object for the specified virtual resources.</returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            string embeddedPath;

            if (!this.TryGetEmbeddedPath(virtualPath, out embeddedPath))
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            return null;
        }

        /// <summary>
        /// Gets a value that indicates whether an embedded resource exists.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns><c>True</c> if the file exists in the virtual file system, or as an embedded resource; otherwise, <c>False</c>.</returns>
        public override bool FileExists(string virtualPath)
        {
            string embeddedPath;

            return base.FileExists(virtualPath)
                || this.TryGetEmbeddedPath(virtualPath, out embeddedPath);
        }

        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>A descendent of the <see cref="System.Web.Hosting.VirtualFile"/> class that represents a file as an embedded resource.</returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (base.FileExists(virtualPath))
                return base.GetFile(virtualPath);

            string embeddedPath = this.GetEmbeddedPath(virtualPath);
            EmbeddedVirtualFile returnValue = null;
            
            if (this.TryGetEmbeddedPath(virtualPath, out embeddedPath))
                returnValue = new EmbeddedVirtualFile(virtualPath, this.embeddedAssembly.GetManifestResourceStream(embeddedPath));
            return returnValue;
        }
        #endregion

        #region Private Methods & Functions
        private bool TryGetEmbeddedPath(string virtualPath, out string embeddedPath)
        {
            embeddedPath = this.GetEmbeddedPath(virtualPath);
            return !string.IsNullOrEmpty(embeddedPath);
        }

        private string GetEmbeddedPath(string virtualPath)
        {
            if (virtualPath.StartsWith("~/"))
                virtualPath = virtualPath.Substring(1);
            if (virtualPath.StartsWith(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath))
                virtualPath = virtualPath.Remove(0, System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.Length);

            virtualPath = virtualPath.ToLowerInvariant().Replace('/', '.');

            return this.embeddedAssembly.GetManifestResourceNames().AsParallel()
                .Where(o => o.EndsWith(virtualPath, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
        #endregion
    }
}
