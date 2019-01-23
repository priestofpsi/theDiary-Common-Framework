using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace System.Web.Compression
{
    public class IISCompressionModule 
        : IHttpModule
    {
        public static IISCompressionModule Register(System.Web.HttpApplication application)
        {
            IISCompressionModule module = new IISCompressionModule();
            module.Init(application);
            return module;
        }

        #region IHttpModule Members
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication application)
        {
            application.EndRequest += this.CompressResponse;
        }
        #endregion

        #region Private Methods & Functions
        private void Application_BeginRequest(object source, EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication) source;
            HttpContext context = application.Context;
        }

        private void CompressResponse(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            CompressionFilter filter = new CompressionFilter(context);
            application.Response.Filter = filter;
        }        
        #endregion        
    }
}
