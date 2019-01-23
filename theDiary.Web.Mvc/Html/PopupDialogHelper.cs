using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class PopupDialogHelper
    {
        private const string RouteName = "ModalDialog";
        
        public static MvcHtmlString PopupDialog<TModel, TProperty>(this HtmlHelper<TModel> html, string actionName, string controllerName)
        {
            return html.PopupDialog<TModel, TProperty>(() => true, actionName, controllerName);
        }

        public static MvcHtmlString PopupDialog<TModel, TProperty>(this HtmlHelper<TModel> html, Func<bool> predicate, string actionName, string controllerName)
        {
            if (RouteTable.Routes[RouteName] == null)
                RouteTable.Routes.Add(RouteName, new Route(string.Format("{0}.axd",RouteName), new PopupDialogJavaScriptHandler()));
            
            if (predicate())
                return new MvcHtmlString("<div id=\"displayModal\" class=\"reveal-modal\" data-reveal></div>");
            return new MvcHtmlString(string.Empty);
        }
    }
}
