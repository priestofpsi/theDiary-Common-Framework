using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace System.Web.Mvc.Html
{
    public static partial class MvcSelectExtensions
    {
        #region Private Constant Declarations
        /// <summary>
        /// The <see cref="String"/> used to format a DropDownList option.
        /// </summary>
        private const string DropDownListDefaultTextFormat = "-- {0} --";

        private const string ConfirmDialogJavascriptSnippet = "$(function () {$('#$ButtonName$').button().click(function (e) {e.preventDefault();$('#confirmDialog').dialog('open');});$('#confirmDialog').dialog({ autoOpen: false, resizable: false, height: 140, modal: true,dialogClass: 'no-close', buttons: [ { text: '$OkText$', 'class': 'button tiny radius', click: function () { $(this).dialog('close'); $('form').submit(); }}, { text: '$CancelText$', 'class': 'button alert tiny radius', click: function () { $(this).dialog('close'); } }]});});";
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


        public static MvcHtmlString ConfirmDialog(this HtmlHelper html, string buttonName, string text, string title, string okButton = "Yes", string cancelButton = "No")
        {
            return new MvcHtmlString(string.Format("<confirmDialog title='{0}' linkedId='{1}' okText='{3}' cancelText='{4}'><p>{2}</p></confirmDialog>", title, buttonName, text, okButton, cancelButton));
        }

        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString PlaceholderTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            string displayName;
            htmlHelper.TryGetDisplayName(expression, false, out displayName);
            return htmlHelper.TextBoxFor(expression, htmlAttributes).AddAttributes(string.Format("placeholder=\"{0}\"", displayName));
        }

        public static MvcHtmlString PlaceholderTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, params string[] htmlAttributes)
        {
            string displayName;
            htmlHelper.TryGetDisplayName(expression, false, out displayName);
            return htmlHelper.EditorFor(expression).AddAttributes(string.Format("placeholder=\"{0}\"", displayName)).AddAttributes(htmlAttributes);
        }

        public static MvcHtmlString PlaceholderDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            string displayName;
            htmlHelper.TryGetDisplayName(expression, false, out displayName);
            selectList = selectList.AppendToBegining(new SelectListItem());
            return htmlHelper.DropDownListFor(expression, selectList, new Dictionary<string, object>() { { "data-placeholder", displayName }, { "class", "chzn-select" } });
        }

        #region Public Methods & Functions
        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName)
        {
            string displayName;

            if (!htmlHelper.TryGetDisplayName(expression, useShortName, out displayName))
                return htmlHelper.DisplayName(displayName);

            return htmlHelper.DisplayNameFor(expression);

            if (useShortName)
            {
                var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(htmlHelper.ViewData.Model.FirstOrDefault()));
                var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
                if (displayMetaData != null)
                    return htmlHelper.DisplayName(displayMetaData.GetShortName());
            }

            return htmlHelper.DisplayNameFor(expression);
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName)
        {
            string displayName;
            if (!htmlHelper.TryGetDisplayName(expression, useShortName, out displayName))
                return htmlHelper.DisplayName(displayName);

            return htmlHelper.DisplayNameFor(expression);
        }

        /// <summary>
        /// Returns a single-selection select element using the specified HTML helper, the name of the form field, and the specified list items, with an initial value optional value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field to return.</param>
        /// <param name="selectList">A collection of <see cref="System.Web.Mvc.SelectListItem"/> objects that are used to populate the drop-down list.</param>
        /// <returns>An HTML select element with an option subelement for each item in the list.</returns>
        public static MvcHtmlString DefaultDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            if (selectList.IsEmpty())
                return htmlHelper.DropDownList(name, selectList, MvcSelectExtensions.GetDropDownListOptionalText());

            return htmlHelper.DropDownList(name, selectList, MvcSelectExtensions.GetDropDownListOptionalText(MvcSelectExtensions.PopulatedDropDownListDefaultText));
        }

        //public static MvcHtmlString DefaultDropDownList(this HtmlHelper htmlHelper, string name)
        //{
        //    return htmlHelper.DefaultDropDownList(name, Enumerable.Empty<SelectListItem>());
        //}

        /// <summary>
        /// Returns an HTML select element for each property in the object that is represented by the specified expression using the specified list items and option label, with an initial value optional value.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="selectList">A collection of System.Web.Mvc.SelectListItem objects that are used to populate the drop-down list.</param>
        /// <returns>An HTML select element for each property in the object that is represented by the expression.</returns>
        public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            if (selectList.IsEmpty())
                return htmlHelper.DropDownListFor(expression, selectList, MvcSelectExtensions.GetDropDownListOptionalText());

            return htmlHelper.DropDownListFor(expression, selectList, MvcSelectExtensions.GetDropDownListOptionalText(MvcSelectExtensions.PopulatedDropDownListDefaultText));
        }

        public static MvcHtmlString DefaultDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            if (typeof(TProperty) == typeof(bool) || typeof(TProperty) == typeof(bool?))
                return htmlHelper.DefaultDropDownListFor(expression, new SelectList(new[] { new { Value = true, Text = "Yes" }, new { Value = false, Text = "No" } }, "Value", "Text"));
            if (typeof(TProperty).IsEnum)
            {
                List<dynamic> values = new List<dynamic>();
                foreach (var t in Enum.GetValues(typeof(TProperty)))
                    values.Add(new { Value = t, Text = Enum.GetName(typeof(TProperty), t) });
                return htmlHelper.DefaultDropDownListFor(expression, new SelectList(values, "Value", "Text"));
            }

            return htmlHelper.DefaultDropDownListFor(expression, Enumerable.Empty<SelectListItem>());
        }

        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string dateFormat = "yyyy-MM-dd")
        {
            return htmlHelper.TextBox(name, dateFormat, new { Class = "text-box single-line user-success", Type = "date" });
        }

        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, DateTime? value, string dateFormat = "yyyy-MM-dd")
        {
            if (value.HasValue)
                return htmlHelper.TextBox(name, value.Value.ToString("{0:" + dateFormat + "}"), new { Class = "text-box single-line user-success", Type = "date" });

            return htmlHelper.DatePicker(name, dateFormat);
        }

        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string dateFormat = "yyyy-MM-dd")
        {
            return htmlHelper.TextBoxFor<TModel, TProperty>(expression, dateFormat, new { Class = "text-box single-line user-success", Type = "date" });
        }

        public static string GetDropDownListOptionalText(this System.Web.Mvc.WebViewPage view, string text = MvcSelectExtensions.EmptyDropDownListDefaultText)
        {
            return MvcSelectExtensions.GetDropDownListOptionalText(text);
        }

        public static MvcHtmlString AddCssClasses(this MvcHtmlString html, params string[] cssClasses)
        {
            if (cssClasses.IsNullOrEmpty())
                return html;

            return html.AddAttribute("class", cssClasses.Concat(' '));
        }

        public static MvcHtmlString AddCssClass(this MvcHtmlString html, string cssClass)
        {
            return html.AddAttribute("class", cssClass);
        }

        public static MvcHtmlString AddAttribute(this MvcHtmlString html, string name)
        {
            return html.AddAttribute(name, string.Empty);
        }

        public static MvcHtmlString AddAttribute(this MvcHtmlString html, string name, string value)
        {
            if (value.IsNull())
                return html;
            value = value.Trim();
            if (!value.IsNullEmptyOrWhiteSpace())
                value = string.Format("=\"{0}\"", value);

            return html.AddAttributes(string.Format("{0}{1}", name, value.Trim()));
        }

        public static MvcHtmlString AddAttributes(this MvcHtmlString html, params string[] attributes)
        {
            if (attributes.IsNullOrEmpty())
                return html;
            HtmlAgilityPack.HtmlNode mainNode = HtmlAgilityPack.HtmlNode.CreateNode(html.ToHtmlString().Trim());
            attributes.ForEach(attribute =>
            {
                string[] av = attribute.Split('=');
                mainNode.SetAttribute(av.First(), av.Last());
            });

            return new MvcHtmlString(mainNode.WriteTo());
        }

        public static MvcHtmlString ActionSplitButton(this HtmlHelper htmlHelper, string text, string action, string controller, string splitTarget)
        {
            return htmlHelper.ActionSplitButton(text, action, controller, splitTarget, null, null);
        }

        public static MvcHtmlString ActionSplitButton(this HtmlHelper htmlHelper, string text, string action, string controller, string splitTarget, object routeData, object htmlAttributes = null)
        {
            var link = htmlHelper.ActionLink("{ActionText}", action, controller, routeData, htmlAttributes).AddCssClasses("button", "split");
            var builder = new HtmlString(string.Format("<span data-dropdown=\"{1}\"></span>{0}</a><br>", text, splitTarget));

            var actionLink = link.ToHtmlString();
            return MvcHtmlString.Create(actionLink.Replace("{ActionText}", builder.ToHtmlString()));

        }

        public static MvcHtmlString DateTimeFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var compilationResult = expression.Compile();
            TValue dateValue = compilationResult((TModel)html.ViewDataContainer.ViewData.Model);
            var body = (MemberExpression)expression.Body;
            return html.TextBox(body.Member.Name, (Convert.ToDateTime(dateValue)).ToString(html.ViewData.ModelMetadata.DisplayFormatString), new { id = body.Member.Name, datepicker = true });
        }
        #endregion

        #region Private Mehtods & Functions
        private static void SetAttribute(this HtmlAgilityPack.HtmlNode node, string name, string value)
        {
            if (name.Equals(value))
                value = string.Empty;
            value = value.Replace("\"", string.Empty).Replace("'", string.Empty);
            if (!node.Attributes.Contains(name))
                node.Attributes.Append(name);

            node.Attributes[name].Value = string.Format("{1} {0}", value, node.Attributes[name].Value).Trim();
        }

        private static bool ContainsAttribute(this MvcHtmlString html, string attributeName)
        {
            HtmlAgilityPack.HtmlNode mainNode = HtmlAgilityPack.HtmlNode.CreateNode(html.ToHtmlString().Trim());
            return mainNode.Attributes.Contains(attributeName);
            /*string[] v1 = html.ToHtmlString().Split(' ');
            foreach (var a in v1)
                if (a.Trim().StartsWith(attributeName.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;*/
        }

        private static bool TryGetModelName<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, out string displayName)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
            if (displayMetaData != null)
            {
                displayName = displayMetaData.GetName();
                return true;
            }
            displayName = metaData.DisplayName ?? metaData.Model as string;
            return false;
        }

        private static bool TryGetModelName<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression, out string displayName)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(htmlHelper.ViewData.Model.FirstOrDefault()));
            var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
            if (displayMetaData != null)
            {
                displayName = displayMetaData.GetName();
                return true;
            }
            displayName = metaData.DisplayName ?? metaData.Model as string;
            return false;
        }

        private static bool TryGetDisplayName<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName, out string displayName)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
            if (displayMetaData != null)
            {
                displayName = useShortName ? displayMetaData.GetShortName() : displayMetaData.GetName();
                return true;// displayMetaData.ResourceType != null;
            }

            displayName = metaData.DisplayName;
            return false;
        }

        private static bool TryGetDisplayName<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName, out string displayName)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>(htmlHelper.ViewData.Model.FirstOrDefault()));
            var displayMetaData = metaData.GetMetaDataAttribute<DisplayAttribute>();
            if (displayMetaData != null)
            {
                displayName = useShortName ? displayMetaData.GetShortName() : displayMetaData.GetName();
                return true;// (displayMetaData.ResourceType != null);
            }

            displayName = metaData.DisplayName;
            return false;
        }

        private static string GetDisplayName<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName)
        {
            string returnValue;
            htmlHelper.TryGetDisplayName(expression, useShortName, out returnValue);
            return returnValue;
        }

        private static string GetDisplayName<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            string returnValue;
            htmlHelper.TryGetModelName(expression, out returnValue);
            return returnValue;
        }

        private static bool IsEmpty(this IEnumerable<SelectListItem> selectList)
        {
            return (selectList == null
                || selectList.Count() == 0);
        }

        private static string GetDropDownListOptionalText(string text = MvcSelectExtensions.EmptyDropDownListDefaultText)
        {
            return string.Format(MvcSelectExtensions.DropDownListDefaultTextFormat, string.IsNullOrWhiteSpace(text) ? MvcSelectExtensions.EmptyDropDownListDefaultText : text);
        }
        #endregion
    }
}
