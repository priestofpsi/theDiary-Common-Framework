using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc.Html
{
    public static class AutoCompleteHelpers
    {
        #region Private Constant Declarations
        private const string AutoCompleteControllerKey = "AutoCompleteController";
        
        private const string AutoCompleteActionKey = "AutoCompleteAction";
        #endregion

        #region Public Extension Methods & Functions
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName)
        {
            string autocompleteUrl = UrlHelper.GenerateUrl(null, actionName, controllerName,
                                                           null,
                                                           html.RouteCollection,
                                                           html.ViewContext.RequestContext,
                                                           includeImplicitMvcValues: true);
            return html.PlaceholderTextBoxFor(expression, new { data_autocomplete_url = autocompleteUrl });
        }

        public static void SetAutoComplete(this ModelMetadata metadata, string controller, string action)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = controller;
            metadata.AdditionalValues[AutoCompleteActionKey] = action;
        }

        public static string GetAutoCompleteUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionKey);
            if (string.IsNullOrEmpty(controller)
                || string.IsNullOrEmpty(action))
            {
                return null;
            }
            return UrlHelper.GenerateUrl(null, action, controller, null, html.RouteCollection, html.ViewContext.RequestContext, true);
        }
        #endregion

        #region Private Extension Methods & Functions
        private static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            dictionary.TryGetValue(key, out value);
            return (string)value;
        }
        #endregion
    }
}
