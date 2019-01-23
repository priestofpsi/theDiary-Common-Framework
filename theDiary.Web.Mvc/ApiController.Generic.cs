using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Web.Http
{
    public class ApiController<T>
        : ApiController
        where T : IDisposable, new()
    {
        #region Private Declarations
        private T context;
        private readonly object syncObject = new object();
        #endregion

        #region Protected Read-Only Properties
        /// <summary>
        /// Gets the <see cref="Context"/> relating to the <see cref="T:Controller"/>.
        /// </summary>
        protected T Context
        {
            get
            {
                lock (this.syncObject)
                {
                    if (this.context == null)
                        this.context = System.Activator.CreateInstance<T>();

                    return this.context;
                }
            }
        }
        #endregion

        #region Protected Methods & Functions
        protected override void Dispose(bool disposing)
        {
            this.Context.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
