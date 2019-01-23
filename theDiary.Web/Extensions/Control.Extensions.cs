using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace System.Web
{
    public static partial class ControlExtensions
    {
        #region Private Constant Declarations
        /// <summary>
        /// The string identifing the attribute name for Css classes.
        /// </summary>
        private const string CssClassName = "class";
        #endregion

        #region HtmlAttribute Methods & Functions
        /// <summary>
        /// Adds a <see cref="HtmlAttribute"/> instance to the <see cref="AttributeCollection"/>.
        /// </summary>
        /// <param name="attributeCollection">The <see cref="AttributeCollection"/> instance to add too.</param>
        /// <param name="attribute">The <see cref="HtmlAttribute"/> to add.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="attribute"/> is <c>Null</c>.</exception>
        public static void Add(this AttributeCollection attributeCollection, HtmlAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            attributeCollection.Add(attribute.Name, attribute.Value);
        }

        /// <summary>
        /// Removes a <see cref="HtmlAttribute"/> instance from the <see cref="AttributeCollection"/>.
        /// </summary>
        /// <param name="attributeCollection">The <see cref="AttributeCollection"/> instance to remove from.</param>
        /// <param name="attribute">The <see cref="HtmlAttribute"/> to remove.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="attribute"/> is <c>Null</c>.</exception>
        public static void Remove(this AttributeCollection attributeCollection, HtmlAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            attributeCollection.Remove(attribute.Name);
        }
        #endregion

        #region Css Methods & Functions
        /// <summary>
        /// Adds a Css class to a <see cref="UserControl"/> if it is not specified.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <param name="cssClass">The name of the Css class to add.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="cssClass"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static void AddCssClass(this UserControl control, string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
                throw new ArgumentNullException("cssClass");

            string cssClassName = control.GetCssAttributeName();
            if (string.IsNullOrWhiteSpace(control.Attributes[cssClassName]))
            {
                control.Attributes[cssClassName] = cssClass.Trim();
            }
            else if (!control.HasCssClass(cssClass))
            {
                control.Attributes[cssClassName] += string.Format(" {0}", cssClass.Trim());
            }
        }

        /// <summary>
        /// Adds a sequence of Css classes to a <see cref="UserControl"/> if it is not specified.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <param name="cssClasses">Sequence of Css classes to add.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="cssClasses"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static void AddCssClasses(this UserControl control, params string[] cssClasses)
        {
            if (cssClasses.IsNullOrEmpty())
                throw new ArgumentNullException("cssClasses");

            cssClasses.Distinct().ForEachAsParallel(cssClass => control.AddCssClass(cssClass));
        }

        /// <summary>
        /// Removes a Css class from a <see cref="UserControl"/> if it is specified.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <param name="cssClass">The name of the Css class to remove.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="cssClass"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static void RemoveCssClass(this UserControl control, string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
                throw new ArgumentNullException("cssClass");

            string cssClassName = control.GetCssAttributeName();
            if (control.HasCssClass(cssClass))
                control.Attributes[cssClassName] = control.Attributes[cssClassName].Replace(cssClass.Trim(), string.Empty).Trim();
        }

        /// <summary>
        /// Removes a sequence of Css classes from a <see cref="UserControl"/> if it is specified.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <param name="cssClasses">Sequence of Css classes to remove.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="cssClasses"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static void RemoveCssClasses(this UserControl control, params string[] cssClasses)
        {
            if (cssClasses.IsNullOrEmpty())
                throw new ArgumentNullException("cssClasses");

            cssClasses.Distinct().ForEachAsParallel(cssClass => control.RemoveCssClass(cssClass));
        }

        /// <summary>
        /// Clears all Css classes assigned to a <see cref="UserControl"/>.
        /// </summary>
        /// <param name="control">The control containing the Css classes.</param>
        public static void ClearCssClasses(this UserControl control)
        {
            string cssClassName = control.GetCssAttributeName();
            control.Attributes.Remove(cssClassName);
        }

        /// <summary>
        /// Determines if a <see cref="UserControl"/> has a specified Css class.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <param name="cssClass">The name of the Css class to locate.</param>
        /// <returns><c>True</c> if the Css class is assigned; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="cssClass"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static bool HasCssClass(this UserControl control, string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
                throw new ArgumentNullException("cssClass");

            string cssClassName = control.GetCssAttributeName();
            if (string.IsNullOrWhiteSpace(control.Attributes[cssClassName]))
                return false;

            return control.Attributes[cssClassName].Contains(cssClass.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region FindControl Methods & Functions
        public static T FindControl<T>(this Control control, string id, bool recursive)
            where T : Control
        {
            return (T)control.FindControl(id, recursive);
        }

        public static Control FindControl(this Control control, string id, bool recursive)
        {
            Control childControl = control.FindControl(id);
            if (childControl != null || !recursive)
                return childControl;

            foreach (Control c in control.Controls)
            {
                childControl = c.FindControl(id, recursive);
                if (childControl != null)
                    return childControl;
            }

            return null;
        }
        #endregion

        #region Private Methods & Functions
        private static string GetCssAttributeName(this UserControl control)
        {
            foreach (string key in control.Attributes.Keys)
                if (key.Equals(ControlExtensions.CssClassName, StringComparison.OrdinalIgnoreCase))
                    return key;

            return ControlExtensions.CssClassName;
        }
        #endregion
    }
}
