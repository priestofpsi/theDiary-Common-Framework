using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static partial class ControllerExtensions
    {
        #region Routing Methods & Functions
        public static void RegisterController(this HttpApplication application, string routeName, string routeArea, Type controllerType)
        {
            application.RegisterController(routeName, routeArea, controllerType, RouteTable.Routes);
        }

        public static void RegisterController(this HttpApplication application, string routeName, string routeArea, Type controllerType, RouteCollection routeCollection)
        {
            if (routeName.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException("routeName");

            string controllerName = controllerType.GetControllerName();
            string routeUrl = controllerType.GetControllerUrlFormat(routeArea, controllerName);
            routeCollection.MapRoute(name: routeName,
                url: routeUrl,
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }, namespaces: new string[] { controllerType.Namespace });
        }

        public static void RegisterController(this HttpApplication application, string routeName, Type controllerType)
        {
            application.RegisterController(routeName, controllerType, RouteTable.Routes);
        }

        public static void RegisterController(this HttpApplication application, string routeName, Type controllerType, RouteCollection routeCollection)
        {
            application.RegisterController(routeName, null, controllerType, routeCollection);
        }

        public static void RegisterController(this HttpApplication application, Type controllerType)
        {
            application.RegisterController(controllerType, RouteTable.Routes);
        }

        public static void RegisterController(this HttpApplication application, Type controllerType, RouteCollection routeCollection)
        {
            application.RegisterController(controllerType.Name, controllerType, routeCollection);
        }

        private static string GetControllerName(this Type controllerType)
        {
            if (!controllerType.Name.EndsWith("Controller"))
                return controllerType.Name;

            return controllerType.Name.Replace("Controller", string.Empty);
        }

        private static string GetControllerUrlFormat(this Type controllerType, string routeArea = null, string controllerName = null)
        {
            System.StringBuilder returnValue = new System.StringBuilder();
            
            if (!routeArea.IsNullEmptyOrWhiteSpace())
                returnValue.Append("/{0}", routeArea);
            
            if (controllerName.IsNull())
                controllerName = controllerType.GetControllerName();
            
            returnValue.Append("/{0}", controllerName);
            returnValue.Append("/{action}/{id}");
            
            return returnValue;
        }
        #endregion

        internal static string GetContentType(this string filePath)
        {
            return filePath.GetContentType(string.Empty);
        }

        internal static string GetContentType(this string filePath, string defaultExtension)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return ControllerExtensions.ReturnExtension(defaultExtension);

            return ControllerExtensions.ReturnExtension(System.IO.Path.GetExtension(filePath));
        }

        internal static string ReturnExtension(string fileExtension)
        {
            switch ((fileExtension ?? string.Empty).ToLower())
            {
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
