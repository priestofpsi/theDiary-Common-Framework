using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    /// <summary>
    /// Adds HTTP headers to the output specifing the caching information used by Browsers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class BrowserCacheAttribute
        : FilterAttribute, IResultFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserCacheAttribute"/> class, specifing that the browser should
        /// not cache the content.
        /// </summary>
        public BrowserCacheAttribute()
            : base()
        {            
            this.Revalidation = HttpCacheRevalidation.ProxyCaches;
            this.Cacheability = HttpCacheability.NoCache;
            this.CacheDuration = new TimeSpan(-1, 0, 0, 0);
            this.MaxProxyAge = TimeSpan.Zero;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserCacheAttribute"/> class.
        /// </summary>
        /// <param name="cacheability"></param>
        /// <param name="revalidation"></param>
        /// <param name="cacheDuration"></param>
        /// <param name="maxProxyAge"></param>
        public BrowserCacheAttribute(HttpCacheability cacheability, HttpCacheRevalidation revalidation, TimeSpan cacheDuration, TimeSpan maxProxyAge)
            : base()
        {
            this.Revalidation = revalidation;
            this.Cacheability = cacheability;
            this.CacheDuration = cacheDuration;
            this.MaxProxyAge = maxProxyAge;
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Returns a value indicating if Caching is enabled/disabled.
        /// </summary>
        public bool DontCache
        {
            get
            {
                return this.Cacheability == HttpCacheability.NoCache
                    || this.Cacheability == HttpCacheability.ServerAndNoCache;
            }
        }

        /// <summary>
        /// Gets the value used on the Cache Control HTTP header.
        /// </summary>
        public HttpCacheability Cacheability { get; private set; }

        /// <summary>
        /// Gets the value used to specify the revalidation of the Cache Control HTTP header.
        /// </summary>
        public HttpCacheRevalidation Revalidation { get; private set; }

        /// <summary>
        /// Gets the value specifing how long the proxy should cache content.
        /// </summary>
        public TimeSpan MaxProxyAge { get; private set; }

        /// <summary>
        /// Gets the value specifing how long the Browser should cache content.
        /// </summary>
        public TimeSpan CacheDuration { get; private set; }
        #endregion

        #region Public Methods & Funbctions
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetRevalidation(HttpCacheRevalidation.ProxyCaches);
            if (this.DontCache)
            {
                cache.SetExpires(DateTime.Now.AddTicks(this.CacheDuration.Ticks * -1));
            }
            else
            {
                cache.SetExpires(DateTime.Now.Add(this.CacheDuration));
            }
            cache.AppendCacheExtension("private");
            cache.AppendCacheExtension("no-cache=Set-Cookie");
            cache.SetProxyMaxAge(this.MaxProxyAge);
        }
        #endregion
    }
}
