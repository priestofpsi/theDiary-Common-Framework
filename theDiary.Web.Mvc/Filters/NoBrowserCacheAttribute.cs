using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    /// <summary>
    /// Specifies that no output should be Cached by the Browser.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NoBrowserCacheAttribute
        : FilterAttribute, IResultFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NoBrowserCacheAttribute"/> class.
        /// </summary>
        public NoBrowserCacheAttribute()
            : base()
        {
        }
        #endregion

        #region Public Methods & Functions
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetRevalidation(HttpCacheRevalidation.ProxyCaches);
            cache.SetExpires(DateTime.Now.AddYears(-5));
            cache.AppendCacheExtension("private");
            cache.AppendCacheExtension("no-cache=Set-Cookie");
            cache.SetProxyMaxAge(TimeSpan.Zero);
        }
        #endregion
    }
}
