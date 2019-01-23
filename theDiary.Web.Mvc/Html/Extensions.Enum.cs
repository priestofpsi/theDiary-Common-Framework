using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System
{
    public static partial class EnumExtensions
    {
        #region Public Static Methods & Functions
        public static IEnumerable<EnumValue> EnumAsEnumerable<TEnum>(this HtmlHelper helper, Expression<Func<EnumValue, dynamic>> orderBy)
        {
            if (orderBy == null)
                throw new ArgumentNullException("orderBy");

            return typeof(TEnum).EnumAsEnumerable().OrderBy(orderBy.Compile());
        }

        public static IEnumerable<EnumValue> EnumAsEnumerable<TEnum>(this HtmlHelper helper)
        {
            return typeof(TEnum).EnumAsEnumerable().OrderBy(a => a.Value);
        }

        public static IEnumerable<EnumValue> EnumAsEnumerable<TEnum>(this Controller controller, Expression<Func<EnumValue, dynamic>> orderBy)
        {
            if (orderBy == null)
                throw new ArgumentNullException("orderBy");

            return typeof(TEnum).EnumAsEnumerable().OrderBy(orderBy.Compile());
        }

        public static IEnumerable<EnumValue> EnumAsEnumerable<TEnum>(this Controller controller)
        {
            return typeof(TEnum).EnumAsEnumerable();
        }
        #endregion

        #region Private Static Methods & Functions
        private static IEnumerable<EnumValue> EnumAsEnumerable(this Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException(string.Format("TEnum '{0}' is not an Enum type.", enumType.Name));

            return enumType.GetFields().Where(a => a.CanEnumerate()).Cast<EnumValue>();
        }

        private static bool CanEnumerate(this FieldInfo enumField)
        {
            return enumField.IsPublic
                && !enumField.IsSpecialName
                && !enumField.IsHidden();
        }

        private static bool IsHidden(this FieldInfo enumField)
        {
            HiddenAttribute hiddenAttrib = enumField.GetAttribute<HiddenAttribute>();
            return (hiddenAttrib != null && !hiddenAttrib.Visible);
        }
        #endregion
    }
}