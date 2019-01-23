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

namespace System.Web.Mvc.Html
{
    public static partial class MvcSelectExtensions
    {
        #region Localize String Methods & Functions
        public static string Localize(this string value)
        {
            return value.Localize(false);
        }

        public static string Localize(this HtmlHelper htmlHelper, string value)
        {
            return value.Localize(false);
        }

        public static string Localize(this string value, bool split)
        {
            if (!split)
                return System.Text.Translation.TranslationController.Instance[value, System.Globalization.CultureInfo.CurrentUICulture];

            StringBuilder sb = new StringBuilder();
            foreach (string val in value.Split(' '))
            {
                sb.Append(System.Text.Translation.TranslationController.Instance[val, System.Globalization.CultureInfo.CurrentUICulture]);
                sb.Append(" ");
            }
            return sb;
        }

        public static string LocalizeFor<TModel>(this HtmlHelper<TModel> htmlHelper, string resourceKey)
        {
            return string.Empty; //ResourceController.Instance.GetString<TModel>(resourceKey);
        }
        #endregion

        #region MvcHtmlString  Methods & Functions
        public static MvcHtmlString LocalizeDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName = false)
        {
            string displayName;
            if (!htmlHelper.TryGetModelName(expression, out displayName))
                displayName = displayName.Localize();

            return new MvcHtmlString(displayName);
        }

        public static MvcHtmlString LocalizeDisplayNameFor<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName = false)
        {
            string displayName;
            if (!htmlHelper.TryGetDisplayName(expression, false, out displayName))
                displayName = displayName.Localize();

            return htmlHelper.DisplayName(displayName);
        }

        public static MvcHtmlString LocalizeDisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool useShortName = false)
        {
            string displayName;
            if (!htmlHelper.TryGetDisplayName(expression, false, out displayName))
                displayName = displayName.Localize();

            return htmlHelper.DisplayName(displayName);
        }
        #endregion

        #region DropDownListOptionalText Mthods & Functions
        public static string LocalizeGetDropDownListOptionalText(this System.Web.Mvc.WebViewPage view, string text = MvcSelectExtensions.EmptyDropDownListDefaultText)
        {
            return MvcSelectExtensions.GetDropDownListOptionalText(text.Localize());
        }
        #endregion
    }
}
