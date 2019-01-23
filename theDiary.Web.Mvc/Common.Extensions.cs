using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System
{

    public static class CommonExtensions
    {


        static CommonExtensions()
        {           
            //ResourceController.Instance.Translate += resourceController_Translate;
        }
        
        private static string resourceController_Translate(object sender, Text.Translation.TranslateEventArgs e)
        {
            return System.Text.Translation.TranslationController.Instance.Service.Translate(e.InputText, e.SourceCulture, e.TargetCulture);
        }

        private static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
        
        #region Private Constant Declarations
        /// <summary>
        /// The <see cref="String"/> used to format a DropDownList option.
        /// </summary>
        private const string DropDownListDefaultTextFormat = "-- {0} --";
        #endregion

        #region Public Constant Declarations
        /// <summary>
        /// The <see cref="String"/> used to represent a populated DropDownList option.
        /// </summary>
        public const string PopulatedDropDownListDefaultText = "Please Select";

        /// <summary>
        /// The <see cref="String"/> used to represent an empty DropDownList option.
        /// </summary>
        public const string EmptyDropDownListDefaultText = "None";
        #endregion

        #region Public Methods & Functions
        //public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName)
        //{
        //    if (useShortName)
        //    {
        //        var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(htmlHelper.ViewData.Model.FirstOrDefault()));
        //        var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
        //        if (displayMetaData != null)
        //            return htmlHelper.DisplayName(displayMetaData.GetShortName());
        //    }

        //    return htmlHelper.DisplayNameFor(expression);
        //}

        //public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName)
        //{
        //    if (useShortName)
        //    {
        //        var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //        var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
        //        if (displayMetaData != null)
        //            return htmlHelper.DisplayName(displayMetaData.GetShortName());
        //    }

        //    return htmlHelper.DisplayNameFor(expression);
        //}

        ///// <summary>
        ///// Returns a single-selection select element using the specified HTML helper, the name of the form field, and the specified list items, with an initial value optional value.
        ///// </summary>
        ///// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        ///// <param name="name">The name of the form field to return.</param>
        ///// <param name="selectList">A collection of <see cref="System.Web.Mvc.SelectListItem"/> objects that are used to populate the drop-down list.</param>
        ///// <param name="sortSelectList">Indicates if the <paramref name="selectList"/> should be sorted.</param>
        ///// <param name="sortAscending">Indicates if the sorting of the <paramref name="selectList"/> should be ascending or descending.</param>
        ///// <returns>An HTML select element with an option subelement for each item in the list.</returns>
        //public static MvcHtmlString DefaultDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, bool sortSelectList = true, bool sortAscending = true)
        //{
        //    string optionalText = CommonExtensions.ProcessDropDownList(ref selectList, sortSelectList, sortAscending);

        //    return htmlHelper.DropDownList(name, selectList, optionalText);
        //}

        //public static MvcHtmlString DefaultDropDownList(this HtmlHelper htmlHelper, string name)
        //{
        //    return htmlHelper.DefaultDropDownList(name, Enumerable.Empty<SelectListItem>());
        //}

        ///// <summary>
        ///// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items and option label, with an initial value optional value.
        ///// </summary>
        ///// <typeparam name="TModel">The type of the model.</typeparam>
        ///// <typeparam name="TProperty">The type of the value.</typeparam>
        ///// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        ///// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        ///// <param name="selectList">A collection of System.Web.Mvc.SelectListItem objects that are used to populate the drop-down list.</param>
        ///// <param name="sortSelectList">Indicates if the <paramref name="selectList"/> should be sorted.</param>
        ///// <param name="sortAscending">Indicates if the sorting of the <paramref name="selectList"/> should be ascending or descending.</param>
        ///// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        //public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool sortSelectList = true, bool sortAscending = true)
        //{
        //    string optionalText = CommonExtensions.ProcessDropDownList(ref selectList, sortSelectList, sortAscending);

        //    return htmlHelper.DropDownListFor(expression, selectList, optionalText);
        //}

        //public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        //{
        //    return htmlHelper.DefaultDropDownListFor(expression, Enumerable.Empty<SelectListItem>());
        //}

        //public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string dateFormat = "yyyy-MM-dd")
        //{
        //    return htmlHelper.TextBox(name, dateFormat, new { Class = "text-box single-line user-success", Type = "date" });
        //}

        //public static string GetDropDownListOptionalText(this System.Web.Mvc.WebViewPage view, string text = CommonExtensions.EmptyDropDownListDefaultText)
        //{
        //    return CommonExtensions.GetDropDownListOptionalText(text);
        //}

        public static string SiteTitle(this System.Web.Mvc.WebViewPage view)
        {
            return System.Configuration.ConfigurationManager.AppSettings["siteTitle"];
        }

        public static string SiteTitle(this System.Web.Mvc.WebViewPage view, params object[] args)
        {
            object[] formatArgs = new object[] { System.Configuration.ConfigurationManager.AppSettings["siteTitle"] };

            return string.Format(System.Configuration.ConfigurationManager.AppSettings["siteTitleFormat"], formatArgs.Concat(args).ToArray());
        }
        
        public static MvcHtmlString DefaultDropDownList(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.DefaultDropDownList(name, Enumerable.Empty<SelectListItem>());
        }

        ///// <summary>
        ///// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items and option label, with an initial value optional value.
        ///// </summary>
        ///// <typeparam name="TModel">The type of the model.</typeparam>
        ///// <typeparam name="TProperty">The type of the value.</typeparam>
        ///// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        ///// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        ///// <param name="selectList">A collection of System.Web.Mvc.SelectListItem objects that are used to populate the drop-down list.</param>
        ///// <param name="sortSelectList">Indicates if the <paramref name="selectList"/> should be sorted.</param>
        ///// <param name="sortAscending">Indicates if the sorting of the <paramref name="selectList"/> should be ascending or descending.</param>
        ///// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        //public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool sortSelectList = true, bool sortAscending = true)
        //{
        //    string optionalText = CommonExtensions.ProcessDropDownList(ref selectList, sortSelectList, sortAscending);

        //    return htmlHelper.DropDownListFor(expression, selectList, optionalText);
        //}

        //public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        //{
        //    return htmlHelper.DefaultDropDownListFor(expression, Enumerable.Empty<SelectListItem>());
       // }
        #endregion

        #region Private Mehtods & Functions
        private static string ProcessDropDownList(ref IEnumerable<SelectListItem> selectList, bool sortSelectList, bool sortAscending)
        {
            string optionalText = selectList.IsEmpty() ? CommonExtensions.GetDropDownListOptionalText() : CommonExtensions.GetDropDownListOptionalText(CommonExtensions.PopulatedDropDownListDefaultText);

            if (!selectList.IsEmpty() && sortSelectList)
                selectList = (sortAscending) ? selectList.OrderBy(item => item.Text) : selectList.OrderByDescending(item => item.Text);

            return optionalText;
        }
        private static bool IsEmpty(this IEnumerable<SelectListItem> selectList)
        {
            return (selectList == null
                || selectList.Count() == 0);
        }

        private static string GetDropDownListOptionalText(string text = CommonExtensions.EmptyDropDownListDefaultText)
        {
            return string.Format(CommonExtensions.DropDownListDefaultTextFormat, string.IsNullOrWhiteSpace(text) ? CommonExtensions.EmptyDropDownListDefaultText : text);
        }

        private static TAttribute GetMetaDataAttribute<TAttribute>(this ModelMetadata metaData)
            where TAttribute : Attribute
        {
            TAttribute returnValue = (TAttribute)metaData.ContainerType.GetProperty(metaData.PropertyName)
                                      .GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();

            if (returnValue != null)
                return returnValue;

            var mdt = metaData.GetMetadataType();
            if (mdt != null)
                returnValue = (TAttribute)mdt.MetadataClassType.GetProperty(metaData.PropertyName).GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();

            return returnValue;
        }

        private static MetadataTypeAttribute GetMetadataType(this ModelMetadata metaData)
        {
            return (MetadataTypeAttribute)metaData.ContainerType.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
        }
        #endregion
    }
}