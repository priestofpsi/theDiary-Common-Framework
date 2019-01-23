using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    public abstract class Controller<TContext>
        : Controller, IDisposable
        where TContext : class, IDisposable, new()
    {
        #region Constructors
        protected Controller()
            : base()
        {
        }
        #endregion

        #region Private Declarations
        private volatile TContext context;
        private readonly object syncObject = new object();
        #endregion

        #region Protected Read-Only Properties
        protected TContext Context
        {
            get
            {
                lock (this.syncObject)
                {
                    if (this.context.IsNull())
                        this.context = new TContext();

                    return this.context;
                }
            }
        }
        #endregion

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (!returnUrl.IsNullEmptyOrWhiteSpace() 
                && this.Url.IsLocalUrl(returnUrl))
                return this.Redirect(returnUrl);

            return this.RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            this.Context.Dispose();
            base.Dispose(disposing);
        }
    }
}
